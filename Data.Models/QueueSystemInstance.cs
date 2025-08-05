using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Data.Models;

public class QueueSystemInstance
{
    public List<User> Users { get; set; }
    public List<QueueEntry> QueueEntries {get; set;}
    public int QueueId { get; set; }
    public QueueSystemInstance()
    {
        Users = new List<User>();
        QueueEntries = new List<QueueEntry>();
    }

    public int GetUserCount()
    {
        int count = 0;
        foreach (User user in Users)
        {
            if (user.Id != 0)
                count++;
        }
        return count;
    }

    public void Remove(User user)
    {
        if (Users.Contains(user))
        {
            user.IsActive = false;
            Users.Remove(user);
        }
    }
    public void Add(User user)
    {
        user.IsActive = true;
        Users.Add(user);
    }

    public void ClearList()
    {
        foreach (User user in Users.ToList())
        {
            Remove(user);
        }
    }
    public bool HasClaimableIndex()
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].Id == 0)
            {
                return true;
            }
        }
        return false;
    }

    public User GetUserByName(string name)
    {
        foreach (var user in Users)
        {
            if (user.Name == name) return user;
        }
        return null;
    }

    public bool IsEnqueued(User? user)
    {
        if(user == null) return false;
        return Users.Contains(user);
    }
    public bool IsEnqueued(string name)
    {
        User? user = User.GetUserByName(name);
        return IsEnqueued(user);
    }

    public int GetClaimableIndex()
    {
        foreach (User user in Users)
        {
            if(user.Id == 0)
            {
                return Users.IndexOf(user);
            }
        }
        return 0;
    }

    public void UpdateQueueEntries()
    {
        QueueEntries.Clear();

        if(GetUserCount() % 2 != 0)
        {
                User user = new User(this);
                user.Enqueue();
        }

        for (int i = 0; i < Users.Count - 1; i+=2)
        {
            if (Users[i].Id != 0 || Users[i + 1].Id != 0)
            {
                QueueEntries.Add(new QueueEntry(Users[i], Users[i + 1]));
            } else
            {
                Users.Remove(Users[i]);
                Users.Remove(Users[i]);
            }
        }
    }


    public void Next()
    {
        if (Users.Count < 2) return;

        User user1 = Users[0];
        User user2 = Users[1];
        Remove(user1);
        Remove(user2);
        Add(user1);
        Add(user2);

    }
}



//public (List<User> EvenUsers, List<User> OddUsers) GetDividedQueue()
//{

//    List<User> even = new();
//    List<User> odd = new();

//    NormalizeQueue();
//    UpdateQueueEntries();

//    for (int i = 0; i < GetUserCount(); i++)
//    {
//        if (i % 2 == 0)
//        {
//            even.Add(Users[i]);
//        }
//        else
//        {
//            odd.Add(Users[i]);
//        }
//    }

//    if(odd.Count < even.Count)
//    {
//        User user = new User(this);
//        user.Enqueue();
//        odd.Add(user);
//    }

//    return (even, odd);
//}

//private void NormalizeQueue()
//{
//    for(int i = 0; i < GetUserCount() - 1; i++)
//    {
//        if (Users[i].Id == 0 && Users[i+1].Id == 0)
//        {
//            Users.Remove(Users[i]);
//            Users.Remove(Users[i+1]);
//        }
//    }
//}