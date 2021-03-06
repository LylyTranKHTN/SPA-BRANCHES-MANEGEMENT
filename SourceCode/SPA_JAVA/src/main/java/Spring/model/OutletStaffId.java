package Spring.model;
// Generated Dec 22, 2018 7:52:59 AM by Hibernate Tools 5.0.6.Final

import javax.persistence.Column;
import javax.persistence.Embeddable;

/**
 * OutletStaffId generated by hbm2java
 */
@Embeddable
public class OutletStaffId implements java.io.Serializable {

	private int outlet;
	private int staff;

	public OutletStaffId() {
	}

	public OutletStaffId(int outlet, int staff) {
		this.outlet = outlet;
		this.staff = staff;
	}

	@Column(name = "outlet", nullable = false)
	public int getOutlet() {
		return this.outlet;
	}

	public void setOutlet(int outlet) {
		this.outlet = outlet;
	}

	@Column(name = "staff", nullable = false)
	public int getStaff() {
		return this.staff;
	}

	public void setStaff(int staff) {
		this.staff = staff;
	}

	public boolean equals(Object other) {
		if ((this == other))
			return true;
		if ((other == null))
			return false;
		if (!(other instanceof OutletStaffId))
			return false;
		OutletStaffId castOther = (OutletStaffId) other;

		return (this.getOutlet() == castOther.getOutlet()) && (this.getStaff() == castOther.getStaff());
	}

	public int hashCode() {
		int result = 17;

		result = 37 * result + this.getOutlet();
		result = 37 * result + this.getStaff();
		return result;
	}

}
