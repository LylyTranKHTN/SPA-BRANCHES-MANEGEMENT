<template>
    <v-container fluid grid-list-md pa-0>
        <!-- Outlet Detail -->
        <v-layout wrap column>
            <v-flex
                class="text-xs-center">
                <span>{{$router.name}}</span>
            </v-flex>

            <!-- Outlet Image -->
            <v-card>
                <v-img
                    :src="'../../static/outlet/outlet.jpg'"
                    height="150"
                >
                </v-img>
            </v-card>

            <v-flex>
                <v-layout row wrap pa-4 justify-center>
                 <!-- name + button book-->
                    <v-flex xs10 sm7>
                        <v-card>
                            <v-btn
                                class="right"
                                color="primary"
                                flat outline
                                @click="booknow()"
                            >
                                Book now
                            </v-btn>
                            <v-layout wrap column pa-2>
                                <!-- name -->
                                <v-flex>
                                    <v-icon>location_city</v-icon>
                                    <span>{{ outlet.name }}</span>
                                </v-flex>
                                <v-flex>
                                    <v-icon>location_on</v-icon>
                                    <span>{{ outlet.address}}</span>
                                </v-flex>
                                <v-flex>
                                    <v-icon>phone</v-icon>
                                    <span>{{ outlet.phone}}</span>
                                </v-flex>
                            </v-layout>
                        </v-card>
                    </v-flex>
                    <!-- Availability + Date Picker -->
                    <v-flex xs10 sm7>
                        <v-card>
                            <v-card-actions>
                            <h3>Availability Check</h3>
                                <v-spacer></v-spacer>
                            <v-dialog
                                    ref="dialog"
                                    v-model="dialog_date"
                                    :return-value.sync="date"
                                    persistent
                                    lazy
                                    full-width
                                    width="290px"
                                >
                                    <v-text-field
                                        slot="activator"
                                        v-model="date"
                                        label="Pick a date"
                                        readonly
                                        hide-details
                                    ></v-text-field>
                                    <v-date-picker v-model="date" scrollable>
                                        <v-spacer></v-spacer>
                                        <v-btn flat color="primary" @click="dialog_date = false">Cancel</v-btn>
                                        <v-btn flat color="primary" @click="$refs.dialog.save(date)">OK</v-btn>
                                    </v-date-picker>
                            </v-dialog>
                            </v-card-actions>
                            <v-layout column wrap>
                                <!-- Status of date chosen -->
                                <v-flex class="text-xs-center" pb-2>
                                    <h2>
                                        {{status.bed}} beds & {{status.therapist}} therapists
                                    </h2>
                                </v-flex>
                                <!-- Remind -->
                                <v-flex class = "text-xs-center">
                                    <span light = "true">Please book as soon as possible.</span>
                                    <v-spacer></v-spacer>
                                    <span light = "true">Availability will change regularly</span>
                                </v-flex>
                            </v-layout>
                        </v-card>
                    </v-flex>

                    <!-- service -->
                    <v-flex xs10 sm7 md7>
                        <v-card>
                            <!-- Service + button View more -->
                            <v-card-actions>
                                <h3>Services</h3>
                                <v-spacer></v-spacer>
                                <v-btn
                                    flat outline
                                    color = "primary"
                                    @click="viewServices(outlet.id)"
                                >
                                    View more
                                </v-btn>
                            </v-card-actions>
                            <!-- List Service -->
                            <v-layout wrap>
                                <v-flex
                                    class="text-xs-center"
                                    xs3 v-for = "service in services" :key="service.index" pb-2>
                                    <span>{{service.name}}</span>
                                </v-flex>
                            </v-layout>
                        </v-card>
                    </v-flex>

                    <!-- Therapist -->
                    <v-flex xs10 sm7 md7>
                        <v-card>
                            <!-- Therapists + button View more -->
                            <v-card-actions>
                                <h3>Therapists</h3>
                                <v-spacer></v-spacer>
                                <v-btn
                                    flat outline
                                    color = "primary"
                                    @click="viewTherapist(outlet.id)"
                                >
                                    View more
                                </v-btn>
                            </v-card-actions>
                            <!-- Therapist List -->
                            <v-layout wrap>
                                <v-flex
                                    class="text-xs-center"
                                    xs4 v-for = "therapist in therapists " :key="therapist.index" pb-2>
                                    <v-layout column>
                                        <v-flex>
                                            <v-avatar
                                            :size="80"
                                            color="red lighten-4"
                                            >
                                                <v-img
                                                    :src="therapist.avatar"
                                                    :alt="therapist.name"
                                                >
                                                </v-img>
                                        </v-avatar>
                                        </v-flex>
                                        <span>{{therapist.name}}</span>
                                    </v-layout>
                                </v-flex>
                            </v-layout>
                        </v-card>
                    </v-flex>

                    <!-- Reviews -->
                    <v-flex xs10 sm7 md7>
                        <v-card>
                            <!-- Review + Add review + View more -->
                            <v-card-actions>
                                <h3>Reviews</h3>
                                <v-spacer></v-spacer>
                                <v-btn
                                    color = "primary"
                                    flat outline
                                    @click="addReview(outlet.id)"
                                >
                                    Add review
                                </v-btn>

                                 <!-- Button View more -->
                                <v-btn
                                    color = "primary"
                                    flat outline
                                    @click="viewReviews()"
                                >
                                    View more
                                </v-btn>
                            </v-card-actions>
                            <!-- Review List -->
                            <v-layout wrap column
                                v-for="review in reviews" :key="review.index">
                                <v-flex>
                                    <v-card>
                                        <v-card-actions >
                                            <h3>{{review.customerName}}</h3>
                                            <v-spacer></v-spacer>
                                            <v-rating
                                                    v-model="review.star"
                                                    color="yellow darken-3"
                                                    background-color="grey darken-1"
                                                    empty-icon="$vuetify.icons.ratingFull"
                                                    half-increments
                                                    hover
                                                ></v-rating>
                                        </v-card-actions>
                                        <v-card-title class="px-2 py-0">
                                            <span>{{review.content}}</span>
                                        </v-card-title>
                                    </v-card>
                                </v-flex>
                                <v-divider></v-divider>
                            </v-layout>
                        </v-card>
                    </v-flex>
            </v-layout>
            </v-flex>

        </v-layout>
    </v-container>
</template>

<script>
import axios from 'axios';
import { mapState, mapActions, mapGetters } from 'vuex'

export default {
    data () {
        return{
            dialog_date: false,
            date: null,
            status:
                {
                    bed: "4",
                    therapist: "5",
                }
        }
    },
    created(){
        this.$store.dispatch('outlet/getOutletByID', this.outlet.id)
			var o_service = {
				idOutlet: this.outlet.id,
				pageSize: 4,
				pageNumber: 1
			}
			this.$store.dispatch('service/getServicesByOutlet',o_service)
			var o_therapist = {
				outletID: this.outlet.id,
				pageSize: 3,
				pageNumber: 1
			}
			this.$store.dispatch('therapist/getTherapistsByOutlet',o_therapist)
			// Chưa có phâng trang
			this.$store.dispatch('outlet/getOutletReview', this.outlet.id)
    },
    computed: {
        ...mapState({
            outlet: state => state.outlet.outlet,
            // services: state => state.service.services,
            // therapists: state => state.therapist.therapists,
            // reviews: state => state.outlet.outletReview,
        }),
        ...mapGetters({
            services: 'service/ServicesInOutletDetail',
            therapists: 'therapist/TherapistsInOutletDetail',
            reviews: 'outlet/ReviewsInOutletDetail'
        })
    },
    methods: {
        booknow(){
            this.$router.push('/add_new_booking')
        },
        viewReviews(){
            this.$router.push('/outlet/reviews')
        },
        addReview(){
            this.$router.push('/outlet/addnewreview')
        },
        
        viewServices(id){
            var o = {
                idOutlet: id,
                pageSize: 5,
				pageNumber: 1
            }
            this.$store.dispatch('service/getServicesByOutlet',o)
            this.$router.push('/services')
        },

        viewTherapist(id){
            var o_therapist = {
				outletID: id,
				pageSize: 3,
				pageNumber: 1
			}
            this.$router.push('/therapists')
            this.$store.dispatch('therapist/getTherapistsByOutlet',o_therapists)
            
        }
    }
}
</script>
