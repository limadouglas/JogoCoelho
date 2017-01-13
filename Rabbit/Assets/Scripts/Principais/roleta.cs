using UnityEngine;
using UnityEngine.UI;

public class roleta : MonoBehaviour {

	private Camera cam;
	private int numero;
	public GameObject seta;
	private float velRotacaoSeta;
	private bool pararSeta;
	private float pararMinimo;
	private float pararMaximo;
	private bool gerarNumero;
	private bool estaConectado;
	private GameObject msg;
	public Text msgTexto;
	public GameObject painelPropaganda;
	private bool iniciar;


	void Start () {

		iniciar = false;
		Invoke ("iniciarRoleta", 0.5f);

		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();
		msg = GameObject.Find ("Mensagem");

		gerarNumero = true;
		pararSeta = false;
		velRotacaoSeta = 40;
		Invoke ("diminuirVelSeta", 1);
		Invoke ("diminuirVelSeta", 2);
		Invoke ("diminuirVelSeta", 3);
		Invoke ("diminuirVelSeta", 4);
		Invoke ("diminuirVelSeta", 4.5f);
		Invoke ("diminuirVelSeta", 5);
		Invoke ("diminuirVelSeta", 5.5f);



		transform.position = cam.transform.position;
	}
	

	void Update () {


		if (iniciar) {

			seta.GetComponent<RectTransform> ().transform.Rotate (new Vector3 (0, 0, velRotacaoSeta));

			if (pararSeta) {
				if (gerarNumero) {
					numero = Random.Range (0, 10);

					if (numero <= 1) {
						pararMinimo = 230;
						pararMaximo = 250;
						print ("perdeu a vida " + numero.ToString ());
					} else if (numero <= 4) {
						pararMinimo = 100;
						pararMaximo = 120;

						print ("ganhou a vida " + numero.ToString ());
					} else {
						pararMinimo = 340;
						pararMaximo = 350;
						estaConectado = verificarConexao ();

						print ("assista uma propaganda para ganhar a vida " + numero.ToString ());
					}

					gerarNumero = false;
				}
			}

			if (seta.GetComponent<RectTransform> ().eulerAngles.z > pararMinimo && seta.GetComponent<RectTransform> ().eulerAngles.z < pararMaximo) {
				velRotacaoSeta = 0;
				seta.GetComponent<RectTransform> ().transform.Rotate (new Vector3 (0, 0, velRotacaoSeta));
				GameObject.Find ("gameEngine").SendMessage ("roletaSemSom");

				if (numero <= 1) 
					Invoke ("perdeuVida", 0.5f);
				else if (numero <= 4) 
					Invoke ("ganhouVida", 0.5f);
				else {
					if (estaConectado)
						Invoke ("ganhouPropaganda", 0.5f);
					else {
						GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Sem Conexão");
						GameObject.Find ("gameEngine").SendMessage ("roletaSomDerrota");
						recusarPropaganda ();
					}
				}                             
			}
		}
			
	}



	void mostrarPropaganda() {
		PlayerPrefs.SetInt ("vida", 1);
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Ganhou uma vida");
		GameObject.Find ("gameEngine").SendMessage ("roletaSomVida");
		Destroy (gameObject);
	}



	void recusarPropaganda() { 
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Fim de Jogo");
		Destroy (gameObject);
	}



	private void diminuirVelSeta(){
		if (velRotacaoSeta > 10)
			velRotacaoSeta -= 10;
		else if (velRotacaoSeta > 4) {
			velRotacaoSeta -= 2f;
		} else 
			pararSeta = true;

	}



	public void perdeuVida() {
		Destroy (gameObject);
		GameObject.Find ("gameEngine").SendMessage ("roletaSomDerrota");
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Fim de Jogo");
	}



	public void ganhouVida() {
		Destroy (gameObject);
		GameObject.Find ("gameEngine").SendMessage ("roletaSomVida");
		PlayerPrefs.SetInt ("vida", 1);
		GameObject.Find ("gameEngine").SendMessage ("msgPerdeu", "Ganhou uma Vida");
	}



	public void ganhouPropaganda() {
		painelPropaganda.SetActive (true);
	}



	private void alterarMensagem(string textoDaMensagem) {
		msg.SetActive (true);
		msgTexto.text = textoDaMensagem;
	}



	private bool verificarConexao() {

		if (Application.internetReachability == NetworkReachability.NotReachable)
			return false;
		else
			return true;		
	}



	private void iniciarRoleta(){
		iniciar = true;
		GameObject.Find ("gameEngine").SendMessage ("roletaSomRodar");

	}



}
