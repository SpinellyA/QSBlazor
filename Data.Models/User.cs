using Microsoft.VisualBasic.FileIO;

namespace Data.Models;

public class User
{
    public QueueSystemInstance Queue { get; set; }
    public List<User> BestFriends { get; set; } // what the fuck
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }
    public static int UserCount { get; set; } = 0;
    public static List<User> Users { get; set; } = new();
    public static User? GetUserByName(string name)
    {
        foreach (var user in Users)
        {
            if (user.Name == name)
                return user;
        }
        return null;
    }

    public User(QueueSystemInstance queue) // creates a "Free Spot" user, not a real user, just a placeholder.
    {
        Queue = queue;
        Name = "Free Spot!";
        Id = 0;
        IsActive = false;
    }
    public User(string name, QueueSystemInstance queue)
    {
        UserCount++;
        Name = name;
        IsActive = false;
        Id = UserCount;
        Queue = queue;
        if(name == "Free Spot!")
        {
            Name = Id.ToString();
        }

        Users.Add(this);
    }
    ~User()
    {
        UserCount--;
        Users.Remove(this);
    }

    public bool IsPlaceHolder()
    {
        return Id == 0;
    }

    public void Enqueue()
    {
        if (IsActive)
            return;

        if (Queue.HasClaimableIndex())
        {
            EnqueueAt(Queue.GetClaimableIndex());
            return;
        }
        Queue.Users.Add(this);
        IsActive = true;
    }
    public void Dequeue()
    {
        if (!IsActive)
            return;

        int index = Queue.Users.IndexOf(this);
        User user = new User(Queue);
        user.EnqueueAt(index);
        IsActive = false;
    }

    public void EnqueueAt(int index)
    {
        if (IsActive)
            return;

        Queue.Users[index] = this;
        IsActive = Id == 0 ? false : true;
    } 



}