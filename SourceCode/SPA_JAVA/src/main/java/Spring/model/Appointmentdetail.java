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
 * Appointmentdetail generated by hbm2java
 */
@Entity
@Table(name = "appointmentdetail", schema = "dbo", catalog = "spaCCH")
public class Appointmentdetail implements java.io.Serializable {

	private AppointmentdetailId id;
	private Double cost;
	private String customersign;
	private Date date;
	private String feedback;
	private String imageafter1;
	private String imageafter2;
	private String imagebefore;
	private String note;
	private Integer bed;
	private int timeslot;
	private int staff;
	private Integer deleted;
	private Date createdate;
	private Date updatedate;
	private Long recordversion;

	public Appointmentdetail() {
	}

	public Appointmentdetail(AppointmentdetailId id, Date date, int timeslot, int staff) {
		this.id = id;
		this.date = date;
		this.timeslot = timeslot;
		this.staff = staff;
	}

	public Appointmentdetail(AppointmentdetailId id, Double cost, String customersign, Date date, String feedback,
			String imageafter1, String imageafter2, String imagebefore, String note, Integer bed, int timeslot,
			int staff, Integer deleted, Date createdate, Date updatedate, Long recordversion) {
		this.id = id;
		this.cost = cost;
		this.customersign = customersign;
		this.date = date;
		this.feedback = feedback;
		this.imageafter1 = imageafter1;
		this.imageafter2 = imageafter2;
		this.imagebefore = imagebefore;
		this.note = note;
		this.bed = bed;
		this.timeslot = timeslot;
		this.staff = staff;
		this.deleted = deleted;
		this.createdate = createdate;
		this.updatedate = updatedate;
		this.recordversion = recordversion;
	}

	@EmbeddedId

	@AttributeOverrides({
			@AttributeOverride(name = "appointment", column = @Column(name = "appointment", nullable = false)),
			@AttributeOverride(name = "service", column = @Column(name = "service", nullable = false)) })
	public AppointmentdetailId getId() {
		return this.id;
	}

	public void setId(AppointmentdetailId id) {
		this.id = id;
	}

	@Column(name = "cost", precision = 53, scale = 0)
	public Double getCost() {
		return this.cost;
	}

	public void setCost(Double cost) {
		this.cost = cost;
	}

	@Column(name = "customersign")
	public String getCustomersign() {
		return this.customersign;
	}

	public void setCustomersign(String customersign) {
		this.customersign = customersign;
	}

	@Temporal(TemporalType.TIMESTAMP)
	@Column(name = "date", nullable = false, length = 23)
	public Date getDate() {
		return this.date;
	}

	public void setDate(Date date) {
		this.date = date;
	}

	@Column(name = "feedback")
	public String getFeedback() {
		return this.feedback;
	}

	public void setFeedback(String feedback) {
		this.feedback = feedback;
	}

	@Column(name = "imageafter1")
	public String getImageafter1() {
		return this.imageafter1;
	}

	public void setImageafter1(String imageafter1) {
		this.imageafter1 = imageafter1;
	}

	@Column(name = "imageafter2")
	public String getImageafter2() {
		return this.imageafter2;
	}

	public void setImageafter2(String imageafter2) {
		this.imageafter2 = imageafter2;
	}

	@Column(name = "imagebefore")
	public String getImagebefore() {
		return this.imagebefore;
	}

	public void setImagebefore(String imagebefore) {
		this.imagebefore = imagebefore;
	}

	@Column(name = "note")
	public String getNote() {
		return this.note;
	}

	public void setNote(String note) {
		this.note = note;
	}

	@Column(name = "bed")
	public Integer getBed() {
		return this.bed;
	}

	public void setBed(Integer bed) {
		this.bed = bed;
	}

	@Column(name = "timeslot", nullable = false)
	public int getTimeslot() {
		return this.timeslot;
	}

	public void setTimeslot(int timeslot) {
		this.timeslot = timeslot;
	}

	@Column(name = "staff", nullable = false)
	public int getStaff() {
		return this.staff;
	}

	public void setStaff(int staff) {
		this.staff = staff;
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
