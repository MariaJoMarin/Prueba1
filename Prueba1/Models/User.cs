using Google.Cloud.Firestore;
using Prueba1.Firebase;

namespace Prueba1.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public int Money  { get; set; }
    }

    public class UserHelper
    {
        public async Task<UserModel> getUserInfo(string email)
        {
            Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("User").WhereEqualTo("email", email);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            Dictionary<string, object> data = querySnapshot.Documents[0].ToDictionary();

            string UserID = querySnapshot.Documents[0].Id;

            UserModel user = new UserModel
            {
                Id = UserID,
                Email = data["email"].ToString(),
                Name = data["name"].ToString(),
                Money = Convert.ToInt32(data["money"]),
            };

            return user;
        }

        public async Task<List<UserModel>> getAllUsers()
        {
            List<UserModel> usersList = new List<UserModel>();
            Query query = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("User");
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            foreach (var item in querySnapshot)
            {
                Dictionary<string, object> data = item.ToDictionary();
                
                string UserID = item.Id.ToString();

                usersList.Add(new UserModel
                {
                    Id = UserID,
                    Email = data["email"].ToString(),
                    Name = data["name"].ToString(),
                    Money = Convert.ToInt32(data["money"]),
                });
            }
            return usersList;
        }

        public async Task<bool> MakeTransaction (UserModel user)
        {
            try
            {
                DocumentReference docRef = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("User").Document(user.Id);
                Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
                {
                    {"email",user.Email },
                    {"name",user.Name},
                    {"money",user.Money}
                };
                WriteResult result = await docRef.UpdateAsync(dataToUpdate);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> UpdateUserInfo (UserModel user)
        {
            try
            {
                DocumentReference docRef = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId).Collection("User").Document(user.Id);
                Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
                {
                    {"email",user.Email },
                    {"name",user.Name},
                    {"money",user.Money}
                };
                WriteResult result = await docRef.UpdateAsync(dataToUpdate);

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
