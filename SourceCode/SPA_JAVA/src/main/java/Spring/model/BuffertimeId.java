package Spring.model;
// Generated Dec 22, 2018 7:52:59 AM by Hibernate Tools 5.0.6.Final

import javax.persistence.Column;
import javax.persistence.Embeddable;

/**
 * BuffertimeId generated by hbm2java
 */
@Embeddable
public class BuffertimeId implements java.io.Serializable {

	private int customertype;
	private int service;

	public BuffertimeId() {
	}

	public BuffertimeId(int customertype, int service) {
		this.customertype = customertype;
		this.service = service;
	}

	@Column(name = "customertype", nullable = false)
	public int getCustomertype() {
		return this.customertype;
	}

	public void setCustomertype(int customertype) {
		this.customertype = customertype;
	}

	@Column(name = "service", nullable = false)
	public int getService() {
		return this.service;
	}

	public void setService(int service) {
		this.service = service;
	}

	public boolean equals(Object other) {
		if ((this == other))
			return true;
		if ((other == null))
			return false;
		if (!(other instanceof BuffertimeId))
			return false;
		BuffertimeId castOther = (BuffertimeId) other;

		return (this.getCustomertype() == castOther.getCustomertype()) && (this.getService() == castOther.getService());
	}

	public int hashCode() {
		int result = 17;

		result = 37 * result + this.getCustomertype();
		result = 37 * result + this.getService();
		return result;
	}

}
