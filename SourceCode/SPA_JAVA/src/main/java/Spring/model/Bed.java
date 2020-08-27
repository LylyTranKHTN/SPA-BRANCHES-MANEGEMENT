package Spring.model;
// Generated Dec 22, 2018 7:52:59 AM by Hibernate Tools 5.0.6.Final

import java.util.Date;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import static javax.persistence.GenerationType.IDENTITY;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

/**
 * Bed generated by hbm2java
 */
@Entity
@Table(name = "bed", schema = "dbo", catalog = "spaCCH")
public class Bed implements java.io.Serializable {

	private Integer id;
	private Integer room;
	private Integer deleted;
	private Date createdate;
	private Date updatedate;
	private Long recordversion;

	public Bed() {
	}

	public Bed(Integer room, Integer deleted, Date createdate, Date updatedate, Long recordversion) {
		this.room = room;
		this.deleted = deleted;
		this.createdate = createdate;
		this.updatedate = updatedate;
		this.recordversion = recordversion;
	}

	@Id
	@GeneratedValue(strategy = IDENTITY)

	@Column(name = "id", unique = true, nullable = false)
	public Integer getId() {
		return this.id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@Column(name = "room")
	public Integer getRoom() {
		return this.room;
	}

	public void setRoom(Integer room) {
		this.room = room;
	}

	@Column(name = "deleted")
	public Integer getDeleted() {
		return this.deleted;
	}

	public void setDeleted(Integer deleted) {
		this.deleted = deleted;
	}

	@Temporal(TemporalType.TIMESTAMP)
	@Column(name = "createdate", length = 23)
	public Date getCreatedate() {
		return this.createdate;
	}

	public void setCreatedate(Date createdate) {
		this.createdate = createdate;
	}

	@Temporal(TemporalType.TIMESTAMP)
	@Column(name = "updatedate", length = 23)
	public Date getUpdatedate() {
		return this.updatedate;
	}

	public void setUpdatedate(Date updatedate) {
		this.updatedate = updatedate;
	}

	@Column(name = "recordversion", precision = 10, scale = 0)
	public Long getRecordversion() {
		return this.recordversion;
	}

	public void setRecordversion(Long recordversion) {
		this.recordversion = recordversion;
	}

}
