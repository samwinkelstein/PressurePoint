using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 1;
	public float rotationSpeed;
	public float dist = 0.5f;
	public float radius = 0.5f;
	public Transform Fluid;
	public GameObject Plasma;
	public Transform EM;
	private GameObject trail;
	private bool begin;
	public float multiRad = 1.0f;
	public int	timer = 500;
	public int	starttimer = 240;
	private int powerTime = 120;
	private int iv = 1;
	private int endTimer = 10;
	SpriteRenderer sprito; 

	private bool wasFirePressed = false;
	private Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D>();
		Plasma.particleSystem.enableEmission = true;
		begin = true;
		trail = GameObject.FindWithTag("trail");
		timer = 1000;
		sprito = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
				timer -= 1;
				if (timer <= 0) {
				endTimer --;
						timer = 0;
						multiRad = 0f;
				Plasma.particleSystem.startColor = new Color (.75f, .2f, .2f, .75f);
				Plasma.particleSystem.startLifetime = .8f;
				Plasma.particleSystem.maxParticles = 60;
				Plasma.particleSystem.startSize = 10.0f;
				Plasma.particleSystem.emissionRate = 30;
				radius = 3;
			sprito.color = new Color(0.2f, 0.1f, 0.1f, 0f);
			iv = 1;
				} else if (timer > 1 && timer % (120) == 0) {
						if (iv > 1) {
								multiRad /= 1.25f;
								iv --;
						}
		}
			if( timer < 20 && timer > 0){
				sprito.color = new Color(0.2f, 0.1f, 0.1f, 1.0f);
				Plasma.particleSystem.startLifetime = .4f;
				Plasma.particleSystem.startSize = .2f;
				multiRad -= (1 / 60);
		} else if (timer < 60 && timer > 0) {
						
			Plasma.particleSystem.startColor =  new Color (1f, 0f, 0f, .25f);
			Plasma.particleSystem.startLifetime = .4f;
			sprito.color = new Color (1f, 0f, 0f, 1f);
						iv = 1;
		} else if (timer < 180 && timer > 0 ) {
			sprito.color = new Color (1.0f, .5f, 0.0f, .5f);
			Plasma.particleSystem.startColor = new Color (1.0f, .5f, 0.0f, 1f);
			Plasma.particleSystem.startLifetime = .6f;
		} else if (timer < 300 && timer > 0) {
			sprito.color = new Color (1.0f,1.0f, 0f, .5f);
			Plasma.particleSystem.startColor = new Color (1.0f,1.0f, .5f, .75f);
				}
			else if(timer >= 500){
			sprito.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			Plasma.particleSystem.startColor = new Color (.0f,1.0f, 1.0f, 1f);
			Plasma.particleSystem.startLifetime = .8f;
			}

		//this.rigid.centerOfMass = transform.position;
				float horiz = Input.GetAxisRaw ("Horizontal");
				float vert = Input.GetAxisRaw ("Vertical");
				if (begin) {
					timer = 1000;
					starttimer -= 1;
						GameObject[] fluids = GameObject.FindGameObjectsWithTag ("particle");
						foreach (GameObject gog in fluids) { 
								Vector2 dx = new Vector2 ((this.transform.position.x - gog.transform.position.x) * 7.0f, (this.transform.position.y - gog.transform.position.y) * 7.0f);
								gog.rigidbody2D.AddForce (dx);
						}
						if(starttimer <= 0){
							begin = false;
						}
				}
				if (horiz != 0) {
						transform.Rotate (0, 0, horiz * Time.deltaTime * 100);
						//this.transform.Rotate(new Vector3(0,0,rotationSpeed*horiz));
				}
				//Vector3 position = this.transform.position;
				//position += this.transform.up*speed*vert;
				//this.transform.position = position;
				//Plasma.particleSystem.enableEmission = false;
				Plasma.particleSystem.startSize = radius * 3;
				bool fire = Input.GetAxisRaw ("Jump") != 0;

				if (fire && !wasFirePressed) {
						begin = false;
				} 	//Plasma.particleSystem.maxParticles = 10;
						//Plasma.particleSystem.enableEmission = true;
				
						//for (float i = 0f; i < .5f; i += 0.3f) {
						Collider2D[] particles = Physics2D.OverlapCircleAll (this.transform.position + this.transform.up * dist, radius, 1 << LayerMask.NameToLayer ("particle"));
						int k = 0;
						foreach (Collider2D c in particles) {
								GameObject.Destroy (c.gameObject);
								if (k <= 6)
										Instantiate (Fluid, new Vector3 (EM.position.x, EM.position.y, 0.0f), Quaternion.identity);
								k++;
					
						}
						rigid.AddForce ((Vector2)transform.up * speed);

						GameObject[] fluid = GameObject.FindGameObjectsWithTag ("particle");
						foreach (GameObject go in fluid) { 


								Vector2 dxx = new Vector2 ((trail.transform.position.x - go.transform.position.x) * 10.0f, (trail.transform.position.y - go.transform.position.y) * 10.0f);
								go.rigidbody2D.AddForce (dxx);
						}
						k = 0;
						foreach (GameObject go in fluid) { 
								float distance = Vector3.Distance (this.transform.position, go.transform.position);
								if (distance > 7) {
										GameObject.Destroy (go);
										if (k <= 20)
												Instantiate (Fluid, new Vector3 (EM.position.x, EM.position.y, 0.0f), Quaternion.identity);
										k++;
	
								} else {
										Vector2 dxx = new Vector2 ((trail.transform.position.x - go.transform.position.x) * 10.0f, (trail.transform.position.y - go.transform.position.y) * 10.0f);
										go.rigidbody2D.AddForce (dxx);
								}
						}
						wasFirePressed = fire;
						//}
						wasFirePressed = false;

						Collider2D[] PulseCrystals = Physics2D.OverlapCircleAll (this.transform.position + this.transform.up * dist, radius + .5f, 1 << LayerMask.NameToLayer ("PulseCrystal"));
						int i = 1;
						foreach (Collider2D pulse in PulseCrystals) { 
								if (multiRad <= 1) {
										multiRad = 1;
								}	
								iv ++;
								timer += (120);
								multiRad *= 1.25f;
								
								Destroy (pulse.gameObject);
						}
						radius = multiRad * .3f;
						speed = (1 * multiRad);
						if (radius > 1) {
								radius = 1;
								speed = 3;
						}
				if(endTimer <= 0){
			Plasma.particleSystem.enableEmission = false;
			Plasma.particleSystem.startColor = new Color (1.0f, .5f, 0.0f, 0f);

		}
				}

}
