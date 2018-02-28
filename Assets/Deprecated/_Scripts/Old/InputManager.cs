using UnityEngine;

public class InputManager
{
    public enum EDirection { None, Positive, Negative }
    EDirection direct = EDirection.None;

    #region Private Inputs
    private float HorizontalAxis { get { return Input.GetAxis("Horizontal"); } }
    private float VerticalAxis { get { return Input.GetAxis("Vertical"); } }
    #endregion

    bool m_DashFlag = false;
    char c = 'e';
    float _tick = 0f;

    #region Constructors
    public InputManager()
    {

    }

    #endregion


    #region Input Getters
    public Vector3 Joystick
    {
        get { return new Vector3(HorizontalAxis, 0f, VerticalAxis); }
        private set { Joystick = value; }
    }

    float timer = 0f;
    public float KeyHeldLength(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            if (timer < 1f)
                timer += Time.deltaTime;
        }
        else
            timer = 0f;

        return timer;
    }

    public bool Dash
    {
        get
        {
            if(Pivot || Joystick.magnitude < 0.3f)
            {
                m_DashFlag = false;
                _tick = 0f;
            }

            if (Joystick.magnitude > 0f && !m_DashFlag)
            {
                if (_tick < 1f)
                    _tick += Time.deltaTime;

                if (Joystick.magnitude > 0.75f && _tick < 0.1f)
                    m_DashFlag = true;
            }
            //else
            //{
                //if (Joystick.magnitude < 0.5f)
                //{
                    //m_DashFlag = false;
                    //_tick = 0f;
                //}
            //}

            return m_DashFlag;
        }
    }

    //public bool Pivot
    //{
    //get
    //{
    //if (direct == EDirection.None)
    //{
    //if (Joystick.x > 0f)
    //direct = EDirection.Positive;
    //else if (Joystick.x < 0f)
    //direct = EDirection.Negative;
    //else
    //direct = EDirection.None;
    //}

    //if (Joystick.x < 0f && direct == EDirection.Positive)
    //{
    //Debug.Log("Direction switch!");
    //direct = EDirection.None;
    //return true;
    //}
    //else if (Joystick.x > 0f && c == 'n')
    //{
    //Debug.Log("Direction switch!");
    //direct = EDirection.None;
    //return true;
    //}
    //return false;
    //}
    //}

    public bool Pivot
    {
        get
        {
            if (c == 'e')
            {
                if (Joystick.x > 0f)
                    c = 'p';
                else if (Joystick.x < 0f)
                    c = 'n';
                else
                    c = 'e';
            }

            if (Joystick.x < 0f && c == 'p')
            {
                Debug.Log("Direction switch!");
                c = 'e';
                return true;
            }
            else if (Joystick.x > 0f && c == 'n')
            {
                Debug.Log("Direction switch!");
                c = 'e';
                return true;
            }
            return false;
        }
    }
    #endregion
}

