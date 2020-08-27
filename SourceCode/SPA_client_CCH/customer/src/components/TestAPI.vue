<template>
    <v-container grid-list-lg>
        <v-layout column wrap>
            <v-flex xs1 class="text-xs-center">
                 <h1>Trang sử dụng để test API</h1>
            </v-flex>
            <v-btn color="success"
            @click="testLogin()">
                Test API
            </v-btn>
        </v-layout>
        <v-layout column wrap>
              <h2>Kết quả sau khi gọi API</h2>

            <!-- sử dụng cú pháp {{tên biến ở trong computed}}-->
            <!-- để hiển thị giá trị của nó. -->
            <!-- ví dụ: {{token}} dùng để gọi giá trị của biến token lưu trong phần computed -->
            <v-divider></v-divider>
            <h3>
                outlet by id
                {{outlet}}
            </h3>
            <h3>
                <br />
                service by outlet
                {{servicebyoutlet}}
                <br />>
                service by id
                {{servicebyid}}
            </h3>

            <h3>
                {{outletByName}}
            </h3>
            <h3>
                {{measurementbycustomer}}
                <br />>
                outlet review
                {{outletReview}}

                therapist service outlet
                {{therapists}}
            </h3>
            <h3>
                booking history by customer
                {{bookingHistory}}
            </h3>
            <h3>
                all outlet
                {{alloutlet}}
            </h3>
            <h3>
                service by name
                {{servicebyname}}
            </h3>
            <h3>
                services type
                {{servicetypes}}
            </h3>
            <h3>
                therapist by name
                {{therapistbyname}}
            </h3>
            <h3>
                booking details
                {{booking}}
            </h3>
        </v-layout>
    </v-container>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
    data() {
        return{

        }
    },

//Cú pháp computed dùng để lấy dữ liệu được lưu trong state của mỗi file index ở thư mục store vừa rồi
     computed: {
        ...mapState({
            // Ví dụ: Trong đó
            // token ở bên trái là biến lưu giá trị
            // do có nhiều state ở các file nên phải đi theo đường dẫn: state.'thư mục chứa state cần lấy'.'dữ liệu trong state'
            // lấy giá trị token lưu ở state của customer.
            outlet: state => state.outlet.outlets,
            servicebyoutlet: state => state.service.services,
            outletReview: state => state.outlet.outlets,
            servicebyid: state => state.service.services,
            outletByName: state => state.outlet.outlets,
            measurementbycustomer: state => state.booking.measurementbycustomer,
            bookingHistory: state => state.booking.bookingHistory,
            therapists: state => state.therapist.therapists,
            numBedTherapist: state => state.service.numBedTherapist,
            alloutlet: state => state.outlet.outlets,
            servicebyname: state => state.service.services,
            servicetypes: state => state.service.serviceTypes,
            therapistbyname: state => state.therapist.therapists,
            booking: state => state.booking.booking
        }),
    },
    methods: {
        // ...mapActions('outlet', ['loadOutlet'])
        async testLogin (){
            console.log('login method')
            this.$store.dispatch('outlet/getOutletByID', '1')
            this.$store.dispatch('service/getServicesByOutlet','1')
            this.$store.dispatch('outlet/getOutletReview', '1')
            this.$store.dispatch('service/getServiceByID', '1')
            this.$store.dispatch('outlet/getOutletByName', 'n')
            
            var payload= {
                cusID: '1',
                serID: '1'
            }
            this.$store.dispatch('booking/getMeasurementByCustomer', payload)
            var o = {idService:"1", idOutlet:"1"}
            this.$store.dispatch('therapist/getTherapistbyServiceOutlet',o)
            var cus_day = {
                cusID: '1',
                dayPassed: '2'
            }
            this.$store.dispatch('booking/getBookingHistoryByCustomer', cus_day)
        
            this.$store.dispatch('outlet/GetNumOfBedAndTherapistByOutlet','1')
        
        
            this.$store.dispatch('outlet/getAllOutlet')
            this.$store.dispatch('service/getServiceByName','his')
            this.$store.dispatch('service/getServiceType')
            this.$store.dispatch('therapist/getTherapistsByName', 'bao')
            this.$store.dispatch('booking/getBookingDetail', '1')
        },
    },
}
</script>
