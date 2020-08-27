<template>
    <v-container grid-list-xs>
        <v-layout row wrap justify-center>
            <v-flex xs10 sm7 md7>
                <v-card>
                    <v-container grid-list-xs fluid>
                        <v-layout row wrap align-center>
                            <v-flex xs3>
                                <v-avatar
                                    size="80"
                                    color="red"
                                >
                                    <img src="src" alt="alt">
                                </v-avatar>
                            </v-flex>
                            <v-flex xs8>
                                <span>{{therapist.name}}</span>
                                <br>
                                <span>Age: {{therapist.dayOfBirth}}</span>
                                <br>
                                <span>Experience: {{therapist.experience}}</span>
                            </v-flex>
                        </v-layout>
                        <v-layout row wrap pt-4>
                            <span>{{therapist.decription}}</span>
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
    data() {
        return {
            //
        }
    },
    computed: {
        ...mapState({
            therapist: state => state.therapist.therapist,
        })
    },
    // for function for event
    methods: {
        loadTherapist(therapistID){
            this.$store.dispatch('outlet/getAllOutlet', therapistID)
        },
    }
}
</script>
