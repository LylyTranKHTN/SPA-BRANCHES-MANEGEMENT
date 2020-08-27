<template>
	<v-container>
		<v-layout>
			<v-text-field
                label="Outlet Search By Name"
                solo
            ></v-text-field>
            <v-btn>
                <v-icon icon>search</v-icon>
            </v-btn>
		</v-layout>
		<v-layout>
			<v-flex xs12 sm12  class="text-xs-center">
      			<v-card>
        			<v-img
				          src="https://i.ytimg.com/vi/ySk55Ufc4RM/maxresdefault.jpg"
				          aspect-ratio="2.75"
        			></v-img>

        		<v-card-title primary-title>
          			<div>
            			<h3 class="headline mb-0">Marvel Spa</h3>       
         			 </div>
        		</v-card-title>

        		<v-card-actions>
          			<v-rating v-model="rating"></v-rating>
          			<div class="ml-2 grey--text text--darken-2">
              			<span>{{ value }}</span>
              			<span>({{ reviews }})</span>
           			 </div>
        		</v-card-actions>
      			</v-card>
    		</v-flex>
		</v-layout>
		<v-layout>
			<v-flex xs12 sm12  class="text-xs-center">
      			<v-card>
        			<v-img
				          src="https://i.ytimg.com/vi/ySk55Ufc4RM/maxresdefault.jpg"
				          aspect-ratio="2.75"
        			></v-img>

        		<v-card-title primary-title>
          			<div>
            			<h3 class="headline mb-0">Marvel Spa</h3>       
         			 </div>
        		</v-card-title>

        		<v-card-actions>
          			<v-rating v-model="rating"></v-rating>
          			<div class="ml-2 grey--text text--darken-2">
              			<span>{{ value }}</span>
              			<span>({{ reviews }})</span>
           			 </div>
        		</v-card-actions>
      			</v-card>
    		</v-flex>
		</v-layout>
		<v-layout >
			
		</v-layout>
	</v-container>
</template>

<script >
	
	export default {
    data () {

        return{
        	reviews: 413,
      		value: 4,
        	rating: 4,
        	review:91,
            OutletName:['outlet 1', 'outlet 2', 'outlet 3']
           
        }
    },
    
    }
</script>