using BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.model;
using Google.Cloud.Firestore;

namespace BANBANH_ORDER_BUT_NOT_BUY_SINGLE_RUN.INCLUDE.SINGLE
{
    public static class USE_ENVIROMENT
    {
        public static void GOOGLE_CREDENTIALS()
        {
            Environment.SetEnvironmentVariable(SETTING.GOOGLE_APPLICATION_CREDENTIALS, SETTING.PATH_CREDENTIALS);
        }

    }

    public static class FIRESTORE_METHOD
    {
        public static CollectionReference ORDER_SESSION_COLLECTION()
        {
            var db = FirestoreDb.Create(SETTING.FIRESTORE_PROJECT_ID).Collection(FIRESTORE_VARIBALE.FC_ORDER_NC);
            return db;
        }

        public static CollectionReference ORDER_ADDED_COLLECTION()
        {
            var db = FirestoreDb.Create(SETTING.FIRESTORE_PROJECT_ID).Collection(FIRESTORE_VARIBALE.BAN_BANH_ORDER);
            return db;
        }

        public static DocumentReference SESSION_USER_ORDER()
        {
            var db = FirestoreDb.Create(SETTING.FIRESTORE_PROJECT_ID).Collection(FIRESTORE_VARIBALE.FC_ORDER_NC).Document(FIRESTORE_VARIBALE.FD_SESSION_UO);
            return db;
        }

        public static DocumentReference ORDER_RECORD()
        {
            var db = FirestoreDb.Create(SETTING.FIRESTORE_PROJECT_ID).Collection(FIRESTORE_VARIBALE.FC_ORDER_NC).Document(FIRESTORE_VARIBALE.FC_ORDER);
            return db;
        }

    }
}
