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
                    @change="chooseOutlet(outlet)"
                    >
                </v-select>
            </v-flex>
        </v-layout>

        <v-layout justify-center pa->
			<v-flex xs10 sm7 dm7>
				<v-text-field
                    hide-details
					v-model="outletName"
					label="Search service by name"
					outline
					append-icon="search"
					@click:append="searchOutlet(outletName)"
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
                                    :items="types"
                                    label="Type"
                                    @mousedown="loadTypes"
                                    @change="chooseType(type)">
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
                                label="Price"
                                @select="choosePrice(price)">
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
                                label="Order By"
                                @select="chooseOrderby(order_by)">
                                </v-select>
                            </v-flex>

                    </v-layout>
                </v-card>
            </v-flex>
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
                                            v-model="service.rating"
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
            outlet: null,
            type:null,
            price:null,
            order_by:null,
            outletName: null,
            // Static Value (Must change by call api
            types: ['a','b','c'],
            prices:['Dưới 100','100-199','200-300'],
            order_by_s:['Increase','Decrease'],

            // Items in List
            serviceItems:[
                {
                    serviceAvar: "A",
                    name: "Service 1",
                    prices: "100$",
                    time:"1h",
                    rating:3,
                },
                {
                    serviceAvar: "B",
                    name: "Service 2",
                    prices: "100$",
                    time:"1h",
                    rating:3,
                },
                {
                    serviceAvar: "C",
                    name: "Service 3",
                    prices: "100$",
                    time:"1h",
                    rating:3,
                }
            ]
        }
    },
    // mounted: {
    //     loadOutLetsID(outlet){
    //         this.$store.dispatch('service/getServicesByOutlet',outlet)
    //     }
    // },
    computed: {
        ...mapState({
            services: state => state.service.services,
            //outlets: state => state.outlet.outlets,
        })
    },
    // for function for event
    methods: {
        // viewServiceDetails(serviceID){
        //     this.$store.dispatch('service/getServiceByID', serviceID)
        //     this.$router.push('/service')
        // },
        // loadOutLets(){
        //    this.$store.dispatch('outlet/getAllOutlet') 
        // },
        // chooseOutlet(outletID){
        //     var o = {
        //         idOutlet: outletID,
        //         pageSize: 5,
		// 		pageNumber: 1
        //     }
        //     this.$store.dispatch('service/getServicesByOutlet',o)
        // },
        // searchOutlet(outletName){
        //     this.$store.dispatch('service/getServiceByName', outletName)
        // },
        // loadTypes(){
        //     this.$store.dispatch('service/getServiceType')
        //}
    }

}
</script>
