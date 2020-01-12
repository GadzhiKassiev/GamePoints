using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    protected enum PlayerMode
    {
        Spacing,
        Waiting,
        Capturing,
        ChangePlayer
    }

    //private GameObject point;
   
    public GameObject player;

    PlayerState ps;
    public bool isFirstPlayerTurn;
   // PlayerMode pm;

    private void Start()
    {
        //point = GetComponent<PaperList>().point;
        ps = new PlayerState(new Spacing(), gameObject);
        gameObject.GetComponent<Player>().Material = Resources.Load("Materials/green") as Material;
        isFirstPlayerTurn = true;
        // pm = PlayerMode.Spacing;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstPlayerTurn == gameObject.GetComponent<Player>().isFirstPlayer)
            ps.DoAction();
    }

 


    public bool checkIfPosEmpty(Vector3 targetPos)
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("point");
        foreach (GameObject current in allPoints)
        {
            if (current.transform.position == targetPos)
            {
                Debug.Log("Already exist");
                return false;
            }
               
        }
        return true;
    }

    


    class PlayerState
    {
        public APlayerState State { get; set; }
        public GameObject GO { get; set; }

        public PlayerState(APlayerState ps, GameObject gameO)
        {
            State = ps;
            GO = gameO;
        }

        public void DoAction()
        {
            State.DoAction(this, GO);
        }
    }

    abstract class  APlayerState
    {
        abstract public void DoAction(PlayerState ps, GameObject go);

        protected int IntagerRound(int roundInt)
        {

            string roundStr = roundInt.ToString();
            roundStr = roundStr.Remove(roundStr.Length - 1) + "0";
            int roundStrInInt = int.Parse(roundStr);
            int round = roundInt - roundStrInInt;
            if (round >= 5)
                return roundStrInInt + 10;
            else
                return roundInt - round;
        }
        //protected bool fromWaitToCap = true;
    }

    class Spacing : APlayerState
    {
        public override void DoAction(PlayerState ps, GameObject go)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickPos.y = IntagerRound((int)Mathf.Round(clickPos.y));
                clickPos.x = IntagerRound((int)Mathf.Round(clickPos.x));
                RaycastHit2D rayHit = Physics2D.Raycast(clickPos, Vector2.zero);
                clickPos.z += 1;
                if (rayHit.transform != null && rayHit.transform.gameObject.name == "Paper")
                {
                    //if (checkIfPosEmpty(clickPos))
                    //{
                    //GameObject temp = Instantiate(go.GetComponent<PaperList>().point, clickPos, go.transform.rotation);
                    //temp.GetComponent<SpriteRenderer>().material = go.GetComponent<PaperList>().Material;                 
                    //go.GetComponent<PaperList>()[(int)(clickPos.x / 10), (int)(clickPos.y / 10)] = temp;
                    go.GetComponent<Player>().CreatePoint(clickPos);
                    ps.State = new Waiting();
                    // }          
                }
            }
        }     
    }

    class Waiting : APlayerState
    {
        public override void DoAction(PlayerState ps, GameObject go)
        {

            if (Input.GetMouseButtonDown(0))
            {
                //RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, LayerMask.GetMask("point"));
                if (rayHit.transform != null 
                    //&& rayHit.transform.gameObject.tag == "point" 
                    && go.GetComponent<Player>().MaterialCompare(rayHit.transform.gameObject.GetComponent<SpriteRenderer>().material))
                {
                    PointCaptureControl.InitializePointCaptureControl();
                    go.GetComponent<Player>().CreatePlayerObjectsByBeginCapture(rayHit.transform.gameObject);
                    //StaticGameObject.endPoint = rayHit.transform.gameObject;
                    //StaticGameObject.newLineGem = new GameObject("MyGameObject");
                    //StaticGameObject.LRend = StaticGameObject.newLineGem.AddComponent<LineRenderer>();
                    //StaticGameObject.pCol = StaticGameObject.newLineGem.AddComponent<PolygonCollider2D>();
                    //StaticGameObject.pointPoligonColliderList = new List<Vector2>();
                    //StaticGameObject.LRend.material = go.GetComponent<Player>().Material;
                    //StaticGameObject.LRend.startWidth = 3;
                    //StaticGameObject.LRend.positionCount = 2;
                    //StaticGameObject.LRend.SetPosition(0, rayHit.transform.gameObject.transform.position);
                    ps.State = new Capturing();
                }               
            }
            else if (Input.GetMouseButton(1))
            {
                ps.State = new ChangePlayer();
                //ps.State = new Spacing();
            }
                
        }
    }

    class Capturing : APlayerState
    {
        public override void DoAction(PlayerState ps, GameObject go)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, LayerMask.GetMask("point"));
                if (rayHit.transform != null 
                    //&& rayHit.transform.gameObject.tag == "point"
                    && go.GetComponent<Player>().MaterialCompare(rayHit.transform.gameObject.GetComponent<SpriteRenderer>().material)
                    && go.GetComponent<Player>().NeighborhoodPoint(rayHit.transform.position)
                    && go.GetComponent<Player>().LastCapturePointPos != rayHit.transform.position)
                {
                    StaticGameObject.LRend.SetPosition(StaticGameObject.LRend.positionCount - 1, rayHit.transform.gameObject.transform.position);
                    if (rayHit.transform.gameObject.transform.position == StaticGameObject.endPoint.transform.position)
                    {
                        PointCaptureControl.ScaleDetection(rayHit.transform.position);
                        //PointCaptureControl.MinMaxPoint(rayHit.transform.position);
                        //StaticGameObject.pointPoligonColliderList.Add(new Vector2(rayHit.transform.gameObject.transform.position.x, rayHit.transform.gameObject.transform.position.y));
                        //StaticGameObject.pCol.SetPath(0, StaticGameObject.pointPoligonColliderList.ToArray());
                        //StaticGameObject.pCol.OverlapPoint(rayHit.transform.gameObject.transform.position);
                        //PointCaptureControl PCC = new PointCaptureControl(StaticGameObject.pointPoligonColliderList);
                        PointCaptureControl PCC = new PointCaptureControl();
                        go.GetComponent<PaperList>().ChangeCapturePointColor(go.GetComponent<Player>().Material, PCC.DetectCapturePoints());
                        foreach (Point p in PCC.DetectCapturePoints())
                            Debug.Log(p.x + " " + p.y);

                        ps.State = new Waiting();   
                    }       
                    else if(go.GetComponent<Player>().NeighborhoodPoint(rayHit.transform.position))
                    {
                        StaticGameObject.LRend.positionCount += 1;
                        PointCaptureControl.ScaleDetection(rayHit.transform.position);
                        //PointCaptureControl.MinMaxPoint(rayHit.transform.position);
                        //StaticGameObject.pointPoligonColliderList.Add(new Vector2(rayHit.transform.gameObject.transform.position.x, rayHit.transform.gameObject.transform.position.y));
                        go.GetComponent<Player>().LastCapturePointPos = go.GetComponent<Player>().LastPointPos;
                        go.GetComponent<Player>().LastPointPos = rayHit.transform.position;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Destroy(StaticGameObject.newLineGem);
                ps.State = new Waiting();
            }
            else
            {
                Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp.z += 1.5f;
                StaticGameObject.LRend.SetPosition(StaticGameObject.LRend.positionCount - 1, temp);
            }
        }
    }

    class ChangePlayer : APlayerState
    {
        public override void DoAction(PlayerState ps, GameObject go)
        {
            go.GetComponent<Player>().ChangePlayer();
            //go.GetComponent<PaperList>().ChangeMaterial();
            ps.State = new Spacing();
        }
    }
}
