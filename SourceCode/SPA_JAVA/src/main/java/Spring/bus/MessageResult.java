package Spring.bus;

public class MessageResult {
	private String msg;
	private Boolean result;
	public String getMsg() {
		return msg;
	}
	public void setMsg(String msg) {
		this.msg = msg;
	}
	public Boolean getResult() {
		return result;
	}
	public void setResult(Boolean result) {
		this.result = result;
	}
	public MessageResult(String msg, Boolean result) {
		super();
		this.msg = msg;
		this.result = result;
	} 
}
