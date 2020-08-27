<template>
<v-container>
    <v-layout wrap column pb-4>
        <!-- info  -->
        <h3>{{customerName}}</h3>
        <h3>{{outletName}}</h3>
        <h3>{{dateServe}} | {{timeServe}} | {{statusBooking}}</h3>
        <h3>Viewing: {{namService}}</h3>

    </v-layout>

    <v-layout wrap>
        <h3>Therapsit note</h3>
        <!-- therapist note -->
    </v-layout>

    <v-layout>
        <v-flex class="text-xs-center">
        <v-textarea
          v-model="therapsitNote"
          outline
          label="Extra note customer key when book"
        ></v-textarea>
        </v-flex>
    </v-layout>

    <v-layout>
       <v-flex>
           <v-btn
            class="right"
             color="primary"
             @click="addReview(therapsitNote)">
             Add new
           </v-btn>
       </v-flex>
    </v-layout>
    
    <v-layout pb-2>
        <h3>Customer feedback</h3>
    </v-layout>
    <v-layout v-for="Cfeedback in Cfeedbacks" :key="Cfeedback.index" pb-4>
        <v-flex xs3 class="text-xs-center"> 
            <v-avatar
                :size="60"
                color="grey lighten-4"
            >
            <v-img :src= Cfeedback.image alt="avatar">
            </v-img>
            </v-avatar>
        </v-flex>
        <v-flex xs9>
            <v-layout wrap column>
                <v-flex>{{Cfeedback.feedback}}</v-flex>
                <v-flex >
                    <span class="right">{{Cfeedback.datefeedback}}</span>
                </v-flex>
            </v-layout>
        </v-flex>
    </v-layout>

</v-container>
</template>
 <script>
 export default {
   props: {
    //  customerName: null,
    //  outletName:null,
    //  dateServe:null,
    //  timeServe :null,
    //  statusBooking:null,
    //  namService:null,
   },
   data () {
     return{
       therapsitNote:null,
       Cfeedbacks:[
           {
               image:'https://kenh14cdn.com/zoom/700_438/ccccccccccccg1jbkOfv1Ncccccccc/Image/2014/06/3rdWeek/trieuledinh-ava-1aed8.jpg',
               feedback:'this is other feback kklef jgjke f efbf wkef wejef fwefwe',
               datefeedback:'12 Aug 2018 ,15.30',
           },
           {
                image:'https://lh3.googleusercontent.com/mhR-K8j8spMOWJ6UUHxVj5A_haAtGTEUJH2b2ey2bk-jBVivZer6cDGKhTyIdtPW7ttrbdb1CWkapAS2R2Z6owYVnpTiNqQS6cVY7Gfy756Txt85pPGjRs9lVDZVeuGEvPhmGi25=w725',
               feedback:'this is other feeack cche wgfwef wwgef weygf wetwf weywef wewef wegef ',
               datefeedback:'12 Aug 2018 ,15.30'
           },
           {
                image:'http://thumb.connect360.vn/unsafe/0x0/imgs.emdep.vn/Share/Image/2016/02/04/b3fb43166d224f4aeb512fcc0cf790529922d121-152502575.jpg',
               feedback:'this is other feedback',
               datefeedback:'12 Aug 2018 ,15.30'
           },
           {
                image:'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQoTPjX9bulIdfMXJ0cTvG42J6ICGfcGnaydGA0D-TvynbF-RSH9A',
               feedback:'this is other feedback',
               datefeedback:'12 Aug 2018 ,15.30'
           },
       ],
        customerName: 'Quang le',
        outletName:'ABC massage',
        dateServe:'09/02/2018',
        timeServe :'13h20',
        statusBooking:'Booked',
        namService:'Massage body',
     }
   },
   methods: {
     addNewNote(content) {
       console.log('Noi dung review: ' + coxntent);
     }
   }
 }
 </script>