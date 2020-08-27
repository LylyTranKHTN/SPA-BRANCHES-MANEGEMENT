package Spring.ModelDTO;

import java.util.ArrayList;
import java.util.Collection;

public class BedDTO {
	private Integer bedID;
	private Integer roomID;
	private Collection<Integer> serviceID;
	private Boolean isEnable;
	
	public Integer getBedID() {
		return bedID;
	}

	public void setBedID(Integer iD) {
		bedID = iD;
	}

	public void setServiceID(Collection<Integer> serviceID) {
		this.serviceID = serviceID;
	}

	public BedDTO() {
		roomID = 0;
		serviceID = new ArrayList<Integer>();
		isEnable = true;
	}

	public Integer getRoomID() {
		return this.roomID;
	}

	public void setRoomID(Integer roomID) {
		this.roomID = roomID;
	}
	
	public Collection<Integer> getServiceID() {
		return this.serviceID;
	}
	
	public void setRoomName(Collection<Integer> serviceID) {
		this.serviceID = serviceID;
	}
	
	public Boolean getIsEnable() {
		return this.isEnable;
	}
	
	public void setIsEnable(Boolean isEnable) {
		this.isEnable = isEnable;
	}
}
