using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	name 				: text
	description	: text
	sprite 			: text
	effect 			: text
	rarity 			: integer (1:C, 2:B, 3:A, 4:S)
	type 				: integer (1:Move, 2:Attack, 3:Cure, 4:Other)
	damage 			: integer
	position 		: text
	range 			: text
*/

public class Chip
{
	public static DataRow GetChip(int id)
	{
		string query = string.Format("select * from chip where id = {0}", id);
		DataTable table = Database.instance.Execute(query);
		return table.Rows[0];
	}

	public static BaseChip GetBaseChip(int id)
	{
		return new BaseChip(GetChip(id));
	}
}
