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
 * Service generated by hbm2java
 */
@Entity
@Table(name = "service", schema = "dbo", catalog = "spaCCH")
public class Services implements java.io.Serializable {

	private Integer id;
	private byte[] image;
	private String name;
	private Integer numoftimeslot;
	private double price;
	private Integer star;
	private String term;
	private Integer servicetype;
	private Integer deleted;
	private Date createdate;
	private Date updatedate;
	private Long recordversion;

	public Services() {
	}

	public Services(double price) {
		this.price = price;
	}

	public Services(byte[] image, String name, Integer numoftimeslot, double price, Integer star, String term,
			Integer servicetype, Integer deleted, Date createdate, Date updatedate, Long recordversion) {
		this.image = image;
		this.name = name;
		this.numoftimeslot = numoftimeslot;
		this.price = price;
		this.star = star;
		this.term = term;
		this.servicetype = servicetype;
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

	@Column(name = "image")
	public byte[] getImage() {
		return this.image;
	}

	public void setImage(byte[] image) {
		this.image = image;
	}

	@Column(name = "name")
	public String getName() {
		return this.name;
	}

	public void setName(String name) {
		this.name = name;
	}

	@Column(name = "numoftimeslot")
	public Integer getNumoftimeslot() {
		return this.numoftimeslot;
	}

	public void setNumoftimeslot(Integer numoftimeslot) {
		this.numoftimeslot = numoftimeslot;
	}

	@Column(name = "price", nullable = false, precision = 53, scale = 0)
	public double getPrice() {
		return this.price;
	}

	public void setPrice(double price) {
		this.price = price;
	}

	@Column(name = "star")
	public Integer getStar() {
		return this.star;
	}

	public void setStar(Integer star) {
		this.star = star;
	}

	@Column(name = "term")
	public String getTerm() {
		return this.term;
	}

	public void setTerm(String term) {
		this.term = term;
	}

	@Column(name = "servicetype")
	public Integer getServicetype() {
		return this.servicetype;
	}

	public void setServicetype(Integer servicetype) {
		this.servicetype = servicetype;
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
