package Spring.bus;

import java.util.ArrayList;
import java.util.Collection;
import org.apache.commons.collections4.IterableUtils;

public class PageableData{
	public static <T> Collection<T> GetPage(Iterable<T> data, int pageSize, int pageNumber)
	 {
		Collection<T>resultData = new ArrayList<T>();
		
		int counter = 0;
		if(!IterableUtils.isEmpty(data)) {
			for (int i=0; i<IterableUtils.size(data);i++) {
			    if(counter>=pageSize*pageNumber && counter<pageSize*(pageNumber+1))
			    {
			    	T temp = IterableUtils.get(data, counter);
			    
			    	resultData.add(temp);
			    }
			    counter++;
			} 
		}
		return resultData;
	 }
	
	 public static <T> Collection<T>  getCollectionFromIteralbe(Iterable<T> itr) 
	 { 
	// Create an empty Collection to hold the result 
		Collection<T> cltn = new ArrayList<T>(); 
		
		// Use iterable.forEach() to 
		// Iterate through the iterable and 
		// add each element into the collection 
		itr.forEach(cltn::add); 
		
		// Return the converted collection 
		return cltn; 
		} 
}
