using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	sprite 			: text
	point				: integer
	hp					: integer
	damage			: integer
	cure				: integer
	move				: integer
*/

public class User
{
#region Static
	private const int USER_ID = 1;
	private static User user;
	public static User GetUser()
	{
		if (user != null) {return user;}

		string query = string.Format("select * from user where id = {0}", USER_ID);
		DataTable table = Database.instance.Execute(query);
		user = new User(table.Rows[0]);
		return user;
	}
#endregion

#region UserData
	public DataRow rawData {get; private set;}
	public int id {get; private set;}
	public string sprite {get; private set;}
	public int point {get; private set;}
	public int hp {get; private set;}
	public int damage {get; private set;}
	public int cure {get; private set;}
	public int move {get; private set;}

	public User(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		sprite = rawData["sprite"].ToString();
		point = (int)rawData["point"];
		hp = (int)rawData["hp"];
		damage = (int)rawData["damage"];
		cure = (int)rawData["cure"];
		move = (int)rawData["move"];
	}
#endregion
}
