package Spring.model;
// Generated Dec 22, 2018 7:52:59 AM by Hibernate Tools 5.0.6.Final

import java.util.Date;
import javax.persistence.AttributeOverride;
import javax.persistence.AttributeOverrides;
import javax.persistence.Column;
import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

/**
 * ServiceBed generated by hbm2java
 */
@Entity
@Table(name = "service_bed", schema = "dbo", catalog = "spaCCH")
public class ServiceBed implements java.io.Serializable {

	private ServiceBedId id;
	private Integer deleted;
	private Date createdate;
	private Date updatedate;
	private Long recordversion;

	public ServiceBed() {
	}

	public ServiceBed(ServiceBedId id) {
		this.id = id;
	}

	public ServiceBed(ServiceBedId id, Integer deleted, Date createdate, Date updatedate, Long recordversion) {
		this.id = id;
		this.deleted = deleted;
		this.createdate = createdate;
		this.updatedate = updatedate;
		this.recordversion = recordversion;
	}

	@EmbeddedId

	@AttributeOverrides({ @AttributeOverride(name = "bed", column = @Column(name = "bed", nullable = false)),
			@AttributeOverride(name = "service", column = @Column(name = "service", nullable = false)) })
	public ServiceBedId getId() {
		return this.id;
	}

	public void setId(ServiceBedId id) {
		this.id = id;
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
