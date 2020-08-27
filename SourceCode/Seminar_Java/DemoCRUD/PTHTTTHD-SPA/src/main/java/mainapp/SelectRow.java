package mainapp;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.springframework.jdbc.core.JdbcTemplate;
import entities.User;
import entities.UserMapper;

import java.sql.SQLException;
import java.util.List;
import java.util.Map;

public class SelectRow {
	public static void main(String[] args) throws SQLException {
		ApplicationContext ctx = new ClassPathXmlApplicationContext("applicationContext.xml");
		JdbcTemplate jdbcTemplate = (JdbcTemplate) ctx.getBean("jdbcTemplate");
		String sql = "SELECT * FROM user_info WHERE address = ?";
		List<Map<String, Object>> rows = jdbcTemplate.queryForList(sql, "USA");
		for (Map<String, Object> row : rows) {
			System.out.println(row.get("id") +"  " + row.get("name") + "  " + row.get("address"));
		}
		
		System.out.println("----------------------------------");
		List<User> listUser2 = jdbcTemplate.query(sql, new UserMapper(), "USA");
		for (User user: listUser2) {
			System.out.println(user.getId() + "  " +user.getName() + "  " + user.getAddress());
		}

		((ClassPathXmlApplicationContext) ctx).close();
	}
}
