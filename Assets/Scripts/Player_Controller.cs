using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Player_Controller : MonoBehaviour {

    public float speed;
    public float sec;

    public Text counttext;
    public Text Wintext;
    public Text timetext;

    public Button exit;
    private Button btn;

    private Rigidbody rb;
    private int count;
    private GameObject[] list;
    private bool time;
    private Vector3 initialPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        Settext();
        Wintext.text = "";
        time = false;

        btn = exit.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        btn.gameObject.SetActive(false);

    }

    void TaskOnClick()
    {
        Application.Quit();
    }

    void Update()
    {
        timetext.text = "Seconds Left: " + Mathf.Round(sec *  100f) / 100f;
        if(!time)
            sec -= Time.deltaTime;

        if(sec < 0)
        {
            GameOver();
        }
    }

    void FixedUpdate()
    {
        float movehor = Input.GetAxis("Horizontal");
        float movevert = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movehor, 0.0f, movevert);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            Settext();
        }
    }
    void Settext()
    {
        counttext.text = "Cubes Collected: " + count.ToString() + "/5";
        if(count >= 5)
        {
            time = true;
            Wintext.text = "You WIN!!";
            btn.gameObject.SetActive(true);
        }
      
    }

    void GameOver()
    {
        Wintext.text = "You LOSE!!";
        time = true;
        list = GameObject.FindGameObjectsWithTag("PickUp");
        foreach(GameObject x in list)
        {
            x.SetActive(false);
        }

        btn.gameObject.SetActive(true);
    }


}
