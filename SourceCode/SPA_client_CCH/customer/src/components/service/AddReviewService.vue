 <template>
  <v-container fluid>
     <!-- Add a reviews -->
    <v-card>
      <v-layout pb-2>
      <h2>Adding a review for: {{service.name}}</h2>
    </v-layout>
   <!-- Choose star, sử dụng component selects-->
    <v-layout justify-center>
      <v-flex xs5 sm5>
        <v-select
          v-model="star"
          outline
          :items="stars"
          label="Choose Star"
        >
        </v-select>
      </v-flex>
    </v-layout>

    <!-- Content -->
    <v-layout justify-center>
      <v-flex xs10 sm8>
        <v-textarea
          v-model="contentReview"
          outline
          label="Review contents"
        ></v-textarea>
      </v-flex>
    </v-layout>

    </v-card>
    <!-- button add -->
    <v-layout wrap >
      <v-flex class="text-xs-center">
        <v-btn
          color="primary"
          @click="addReview()"
        >
          Add
        </v-btn>
      </v-flex>
    </v-layout>
  </v-container>
 </template>
 <script>
  import axios from 'axios';
  import { mapState, mapActions } from 'vuex'

 export default {
   props: {
     outletName: null
   },
   data () {
     return{
       stars: [1, 2, 3, 4, 5],
       star: '',
       contentReview: ''
     }
   },
   computed: {
        ...mapState({
            customer: state => state.customer.userInfo,
            outlet: state => state.outlet.outlet,
            service: state => state.service.service
        }),
    },
   methods: {
     addReview() {
       var o = {
        serviceId: this.service.id,
	      customerid: 1,
	      star: this.star,
	      content: this.contentReview
       }
      console.log('method add review service')
      this.$store.dispatch('service/addReview', o)
     }
   }
 }
 </script>
