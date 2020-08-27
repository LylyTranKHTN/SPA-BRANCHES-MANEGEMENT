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
                    src= service.image
                    height="150"
                >
                </v-img>
            </v-card>

            <v-flex>
                <v-layout row wrap pa-4 justify-center>
                    <v-flex xs10 sm7 md7>
                        <v-card>
                            <v-container grid-list-xs>
                                <v-layout column wrap>
                                    <v-flex pa-0>
                                            <span>{{service.name}}</span>
                                        </v-flex>
                                    <v-flex>
                                    <v-layout row  wrap align-center>
                                        <span class="pr-4">{{service.price}}</span>
                                        <span>{{service.time}}</span>
                                        <v-spacer></v-spacer>
                                        <v-rating
                                            class="pl-0"
                                            v-model="service.star"
                                            half-increments
                                            color="yellow darken-3"
                                            background-color="grey darken-1"
                                            empty-icon="$vuetify.icons.ratingFull">
                                        </v-rating>
                                        <span></span>
                                    </v-layout>
                                    <v-layout row wrap>
                                        <span>{{service.contents}}</span>
                                    </v-layout>
                                    </v-flex>
                                </v-layout>
                            </v-container>
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
                                    @click="addReview()"
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
                                                    :v-model="review.rating"
                                                    color="yellow darken-3"
                                                    background-color="grey darken-1"
                                                    empty-icon="$vuetify.icons.ratingFull"
                                                    half-increments
                                                    hover
                                                ></v-rating>
                                        </v-card-actions>
                                        <v-card-title class="px-2 py-0">
                                            <span>{{review.contents}}</span>
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
import { mapState, mapActions } from 'vuex'
export default {
    data () {
        return{
            // service: {
            //     name: 'Quận 1',
            //     image: '',
            //     prices: '100$',
            //     time: '50',
            //     contents: 'Đây là nội dung của của service 1'
            // }
        }
    },
    computed: {
        ...mapState({
            service: state => state.service.service
        }),
    },
    methods: {
        viewReviews(){
            this.$router.push('/view_reviews')
        },
        addReview(){
            this.$router.push('/addnewreview')
        },
        viewServices(){
            this.$router.push('/service_list')
        },
        
    }
}
</script>
