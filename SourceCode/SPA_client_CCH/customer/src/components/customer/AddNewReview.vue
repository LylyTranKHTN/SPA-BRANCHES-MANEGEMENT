 <template>
  <v-container fluid grid-list-lg>
     <!-- Add a reviews -->
    <h2 class="py-2">Adding a review for: {{outletName}}</h2>
    <v-card>
      <!-- Choose star, sử dụng component selects-->
      <v-layout justify-center row wrap>
        <v-flex xs6>
          <v-select
            v-model="star"
            outline
            :items="stars"
            label="Choose Star"
            hide-details
          >
          </v-select>
        </v-flex>
        <v-flex xs10>
          <v-textarea
            v-model="contentReview"
            outline
            label="Review contents"
            no-resize
            hide-details
          ></v-textarea>
        </v-flex>
      </v-layout>

      <!-- Content -->
      <v-layout>

      </v-layout>

    </v-card>
    <!-- button add -->
    <v-layout wrap >
      <v-flex class="text-xs-center">
        <v-btn
          flat outline
          color="primary"
          @click="addReview(contentReview, star)"
        >
          Add
        </v-btn>
      </v-flex>
    </v-layout>
  </v-container>
 </template>
 <script>
 export default {
   props: {
     outletName: null
   },
   data () {
     return{
       stars: [1, 2, 3, 4, 5],
       star: null,
       contentReview: null,
     }
   },
   methods: {
     addReview(coxntent, star) {
       console.log('Noi dung review: ' + coxntent + '\n Star: ' + star);
     }
   }
 }
 </script>
