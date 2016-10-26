using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Rights.Entity.Attributes;

/// <summary>
/// 
/// </summary>
namespace Rights.Entity.Db
{
	[Serializable]
	[DataContract(IsReference = true)]
	[Table("dbo.t_rights_user_role")]
	public partial class TRightsUserRole
	{
		public TRightsUserRole()
		{
			
		}

		/// <summary>
		/// 主键
		/// </summary>
		[DataMember]
		[Column("id", ColumnCategory=Category.IdentityKey)]
		public int Id { get; set; }
		
		/// <summary>
		/// 用户id
		/// </summary>
		[DataMember]
		[Column("user_id")] 
		public int? UserId { get; set; }
		
		/// <summary>
		/// 角色id
		/// </summary>
		[DataMember]
		[Column("role_id")] 
		public int? RoleId { get; set; }
		
	}
}