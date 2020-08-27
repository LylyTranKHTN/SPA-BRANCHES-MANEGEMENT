package mainapp;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.springframework.jdbc.core.JdbcTemplate;

import java.sql.SQLException;

public class DeleteRow {
	public static void main(String[] args) throws SQLException {
		ApplicationContext ctx = new ClassPathXmlApplicationContext("applicationContext.xml");
		JdbcTemplate jdbcTemplate = (JdbcTemplate) ctx.getBean("jdbcTemplate");
		String sql = "DELETE FROM user_info_test4 WHERE address = 'Japan';";
		jdbcTemplate.update(sql);
		((ClassPathXmlApplicationContext) ctx).close();
		System.out.println("Deleted!");
	}
}
