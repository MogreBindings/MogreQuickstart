using System;

using Mogre;

using Quickstart2010.Modules;

namespace Quickstart2010.States
{
    /************************************************************************/
    /* program state that just shows a turning ogre head                    */
    /************************************************************************/
    public class TurningHead : State
    {
        //////////////////////////////////////////////////////////////////////////
        private StateManager mStateMgr;

        //////////////////////////////////////////////////////////////////////////
        private Light mLight1;
        private Light mLight2;
        private SceneNode mOgreHead;

        /************************************************************************/
        /* constructor                                                          */
        /************************************************************************/
        public TurningHead()
        {
            mStateMgr = null;
            mLight1 = null;
            mLight2 = null;
            mOgreHead = null;
        }

        /************************************************************************/
        /* start up                                                             */
        /************************************************************************/
        public override bool Startup(StateManager _mgr)
        {
            // store reference to the state manager
            mStateMgr = _mgr;

            // get reference to the ogre manager
            OgreManager engine = mStateMgr.Engine;

            // create one bright front light
            mLight1 = engine.SceneMgr.CreateLight("LIGHT1");
            mLight1.Type = Light.LightTypes.LT_POINT;
            mLight1.DiffuseColour = new ColourValue(1.0f, 0.975f, 0.85f);
            mLight1.Position = new Vector3(-70.5f, 150.0f, 250.0f);
            engine.SceneMgr.RootSceneNode.AttachObject(mLight1);

            // and a darker back light
            mLight2 = engine.SceneMgr.CreateLight("LIGHT2");
            mLight2.Type = Light.LightTypes.LT_POINT;
            mLight2.DiffuseColour = new ColourValue(0.1f, 0.15f, 0.3f);
            mLight2.Position = new Vector3(150.0f, 100.0f, -400.0f);
            engine.SceneMgr.RootSceneNode.AttachObject(mLight2);

            // create the ogre head and add the object to the current scene
            mOgreHead = engine.CreateSimpleObject("Ogre", "ogrehead.mesh");
            engine.AddObjectToScene(mOgreHead);

            // place the camera to a better position
            engine.Camera.Position = new Vector3(0.0f, 25.0f, 100.0f);
            engine.Camera.LookAt(new Vector3());

            // OK
            return true;
        }

        /************************************************************************/
        /* shut down                                                            */
        /************************************************************************/
        public override void Shutdown()
        {
            // check if the state was initialized before
            if (mStateMgr == null)
                return;

            // get reference to the ogre manager
            OgreManager engine = mStateMgr.Engine;

            // check if ogre head exists
            if (mOgreHead != null)
            {
                // remove ogre head from scene and destroy it
                engine.RemoveObjectFromScene(mOgreHead);
                engine.DestroyObject(mOgreHead);
                mOgreHead = null;
            }

            // check if light 2 exists
            if (mLight2 != null)
            {
                // remove light 2 from scene and destroy it
                engine.SceneMgr.RootSceneNode.DetachObject(mLight2);
                engine.SceneMgr.DestroyLight(mLight2);
                mLight2 = null;
            }

            // check if light 1 exists
            if (mLight1 != null)
            {
                // remove light 1 from scene and destroy it
                engine.SceneMgr.RootSceneNode.DetachObject(mLight1);
                engine.SceneMgr.DestroyLight(mLight1);
                mLight1 = null;
            }
        }

        /************************************************************************/
        /* update                                                               */
        /************************************************************************/
        public override void Update(long _frameTime)
        {
            // check if ogre head exists
            if (mOgreHead != null)
            {
                // rotate the ogre head a little bit
                mOgreHead.Rotate(Vector3.UNIT_Y, new Radian(new Degree(0.5f)));
            }

            // check if the escape key was pressed
            if (mStateMgr.Input.WasKeyPressed(MOIS.KeyCode.KC_ESCAPE))
            {
                // quit the application
                mStateMgr.RequestShutdown();
            }
        }

    } // class

} // namespace
