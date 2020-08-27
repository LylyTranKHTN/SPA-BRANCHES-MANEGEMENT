<template>
    <v-container fluid grid-list-lg>
        <!-- Review List -->
        <v-layout column wrap>
            <v-flex>
                <h2>Reviews outlet: {{outlet.name}}</h2>
            </v-flex>

        </v-layout>
        <v-layout row wrap justify-center
            v-for="review in reviews" :key="review.index">
            <v-flex xs10 sm6 md6>
                <v-card>
                    <v-card-actions >
                        <h3>{{review.name}}</h3>
                        <v-spacer></v-spacer>
                        	<v-rating
                                v-model="review.star"
                                half-increments
                                color="yellow darken-3"
                                background-color="grey darken-1"
                                empty-icon="$vuetify.icons.ratingFull">
                            </v-rating>
                    </v-card-actions>
                    <v-card-title class="px-2 py-0">
                        <span>{{review.content}}</span>
                    </v-card-title>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import axios from 'axios';
import { mapState, mapActions } from 'vuex'

export default {
    data(){
        return{
        }
    },
    created() {
        this.$store.dispatch('outlet/getOutletReview', this.outlet.id)
    },
    computed: {
        ...mapState({
            outlet: state => state.outlet.outlet,
            reviews: state => state.outlet.outletReview,
        }),
    },

}
</script>
