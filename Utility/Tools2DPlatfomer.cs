using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dnx2DPlatfomerTools {

    public class Tools2DPlatfomer
    {
        public static bool TargetisInRangeHorizontal(Transform character, Transform target, float range)
        {
            if (character.position.x >= target.position.x)
            {
                if ((character.position.x - target.position.x) < range)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((target.position.x - character.position.x) < range)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Target is in range vertical and full


        public static float DirectionToTargetHorizontal(Transform character, Transform target)
        {
            float charaterDirection = 0;

            if (character.position.x < target.position.x)
            {
                charaterDirection = 1;
            }
            else
            {
                charaterDirection = -1;
            }

            return charaterDirection;

        }

        public static float DirectionToTargetHorizontal(Transform character, Transform target, float toleranceRange)
        {
            float charaterDirection = 0;

            if (character.position.x < target.position.x)
            {
                charaterDirection = 1;
            }
            else
            {
                charaterDirection = -1;
            }

            if (character.position.x >= target.position.x)
            {
                if ((character.position.x - target.position.x) < toleranceRange)
                {
                    charaterDirection = 0;
                }
            }
            else
            {
                if ((target.position.x - character.position.x) < toleranceRange)
                {
                    charaterDirection = 0;
                }
            }

            return charaterDirection;

        }

        //VerticalDirection

        //Full direction

        public static float GetAngleToTarget(Transform origin, Transform target, bool facingRight)
        {
            Vector3 metalPosition = target.position;
            Vector3 charPosition = origin.position;
            float angleY;
            float angleX;
            float angle = 0;

            //if the character position is lower than the metal position on the x axis 
            //adjust the angle's values according to the positions and set the offset on x to half the base of the collider 
            if (metalPosition.x > charPosition.x)
            {
                angleX = (metalPosition.x - charPosition.x);
                angleY = (metalPosition.y - charPosition.y);
            }
            else
            {
                angleX = (charPosition.x - metalPosition.x);
                angleY = (charPosition.y - metalPosition.y);
            }

            //Calculate the angle for the collider and set it to degrees
            angle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg;

            // if the character is flipped use -angle, else invert the angle
            if (!facingRight)
            {
                angle *= -1;
            }
            else
            {
                angle = (angle + 180) % 360;
            }

            return angle;

        }

        public static float CharMinimunDistance(GameObject body)
        {
            BoxCollider2D collider;

            if (body.TryGetComponent(out collider))
            {
                return collider.size[0] / 2;
            }
            else
            {
                Debug.LogWarning("Char has no BoxCollider2D, returning default 0.5f");
                return 0.5f;
            }
        }

    }
}
