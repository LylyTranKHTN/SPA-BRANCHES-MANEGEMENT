<template>
    <v-container grid-list-lg fluid>
        <!-- Choose Oulet -->
        <v-layout justify-center>
            <v-flex xs10 sm7 md7>
                <v-select
                    
                    hide-details
                    v-model="outlet"
                    outline
                    :items="outlets"
                    item-text="name"   
                    item-value="id"
                    label="Choose Outlet"
                    @mousedown="loadOutLets"
                    @change="loadServiceByOuletID()"
                    >
                </v-select>
            </v-flex>
        </v-layout>

        <v-layout justify-center pa->
			<v-flex xs10 sm7 dm7>
				<v-text-field
                    hide-details
					v-model="name"
					label="Search service by name"
					outline
					append-icon="search"
					@click:append="search()"
				></v-text-field>
			</v-flex>
		</v-layout>

        <v-layout row wrap justify-center>
            <v-flex xs10 sm7 md7>
                <v-card>
                    <!-- Choose: Type - Price - Order by -->
                    <v-layout justify-center row>
                        <!-- Type -->
                            <v-flex>
                                <v-select
                                    class="pl-2"
                                    hide-details
                                    v-model="type"
                                    outline
                                    :items="serviceTypes"
                                    item-text="name"
                                    item-value="id"
                                    label="Type"
                                    @mousedown="loadTypes"
                                    @change="search()"
                                    >
                                </v-select>
                            </v-flex>
                        <!-- Price -->
                            <v-flex
                                class="px-0">
                                <v-select
                                hide-details
                                v-model="price"
                                outline
                                :items="prices"
                                item-text="name"
                                item-value="id"
                                label="Price"
                                @change="search()">
                                </v-select>
                            </v-flex>


                        <!-- Orderby -->

                            <v-flex>
                                <v-select
                                class="pr-2"
                                hide-details
                                v-model="order_by"
                                outline
                                :items="order_by_s"
                                item-text="name"
                                item-value="id"
                                label="Order By"
                                @change="search()">
                                </v-select>
                            </v-flex>

                    </v-layout>
                </v-card>
            </v-flex>
        </v-layout>

        <!-- Loading -->
        <v-layout
            v-if="loading"
            column fill-height align-center justify-center px-2 pt-0>
            <v-progress-linear
                :indeterminate="loading"
                height="5"
            >              
            </v-progress-linear>
        </v-layout>

        <!-- Shownoti -->
        <v-layout row wrap v-if="noti != null">
                    <app-alert
                        :noti="noti"
                        @dismissed="onDismissed"
                        >
                    </app-alert>
                </v-layout>


        <!-- Show List View -->
        <v-layout  
            v-for="service in services" :key="service.index"
            @mousedown="viewServiceDetails(service.id)"
            wrap row justify-center>
            <v-flex xs10 sm7 md7>
                <v-card>
                    <v-container grid-list-xs>
                        <v-layout row wrap>
                            <v-flex xs3>
                                <v-avatar
                                    size="65"
                                    color="red"
                                >
                                    <v-img :src="service.image" alt="alt">
                                    </v-img>
                                </v-avatar>
                            </v-flex>
                            <v-flex>
                               <v-layout row  column>
                                   <v-flex pa-0>
                                       <span>{{service.name}}</span>
                                       <br>
                                        <span class="pr-4">S$ {{service.price}}</span>
                                        <span>{{service.numOfTimeSlot}} mins</span>
                                   </v-flex>
                                    <v-flex pa-0>
                                        <v-rating
                                            class="pl-0"
                                            v-model="service.star"
                                            half-increments
                                            color="yellow darken-3"
                                            background-color="grey darken-1"
                                            empty-icon="$vuetify.icons.ratingFull">
                                        </v-rating>
                                        <span></span>
                                    </v-flex>
                               </v-layout>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'

export default {
    // for data from other layout
    props: {

    },
    // for data in this layout
    data(){
        return {
            // Choosed Value
            type:null,
            price:null,
            order_by:null,
            name: null,
            // Static Value (Must change by call api
            prices:[
                {id: null, name: 'None'},
                {id: 1,name:'Dưới 100'},
                {id: 2,name:'100-199'},
                {id: 3,name:'200-300'},
                {id: 4,name:'Trên 300'}],
            order_by_s:[{id: null, name: 'None'},{id: 1,name:'Increase'},{id: 0,name:'Decrease'}],
        }
    },
    created(){
        this.loadOutLets()
    },
    // mounted: {
    //     loadOutLetsID(outlet){
    //         this.$store.dispatch('service/getServicesByOutlet',outlet)
    //     }
    // },
    computed: {
        ...mapState({
            services: state => state.service.services,
            outlets: state => state.outlet.outlets,
            serviceTypes: state => state.service.serviceTypes,
            loading: state => state.share.loading,
            noti: state => state.share.noti,

            // outlet: state => state.outlet.outlet
        }),
        outlet: {
            get() {
                return this.$store.state.outlet.outlet
            },
            set(newID) {
                this.$store.dispatch('outlet/changeIDCurrentOutLet', newID)
            }
        },
    },
    // for function for event
    methods: {
        viewServiceDetails(serviceID){
            this.$store.dispatch('service/getServiceByID', serviceID)
            this.$router.push('/service')
        },
        loadOutLets(){
           this.$store.dispatch('outlet/getAllOutlet') 
        },
        loadServiceByOuletID(){
            var object = {
                idOutlet: this.outlet.id,
                pageSize: '',
                pageNumber: ''
            }
            this.$store.dispatch('service/getServicesByOutlet', object)
        },
        search(){
            var object = {
                outletID: this.outlet.id,
                serviceName: this.name,
                serviceType: this.type,
                servicePrice: this.price,
                orderBy: this.order_by
            }
            console.log('outlet: ' + this.outlet.id)
            console.log('type '  +this.type)
            this.$store.dispatch('service/getServiceFilter', object)
        },
        loadTypes(){
            this.$store.dispatch('service/getServiceType')
        },
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }

    }

}
</script>
