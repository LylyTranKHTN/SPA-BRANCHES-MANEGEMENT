package Spring.ModelDTO;


public class BedRoomDTO {
	private Integer bedID;
	private Integer roomID;
	private String roomName;
	private Boolean isEnable;
	
	public BedRoomDTO() {
		bedID = 0;
		roomID = 0;
		roomName = "";
		isEnable = true;
	}
	
	public Integer getbedId() {
		return this.bedID;
	}

	public void setbedId(Integer bedID) {
		this.bedID = bedID;
	}

	public Integer getRoomID() {
		return this.roomID;
	}

	public void setRoomID(Integer roomID) {
		this.roomID = roomID;
	}
	
	public String getRoomName() {
		return this.roomName;
	}
	
	public void setRoomName(String roomName) {
		this.roomName = roomName;
	}
	
	public Boolean getIsEnable() {
		return this.isEnable;
	}
	
	public void setIsEnable(Boolean isEnable) {
		this.isEnable = isEnable;
	}
}


