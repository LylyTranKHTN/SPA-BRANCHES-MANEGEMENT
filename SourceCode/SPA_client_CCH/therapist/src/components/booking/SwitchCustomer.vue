<template>
        <v-flex>
            <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
            <!-- Button Active -->
            <v-btn
                slot="activator" color="primary" dark
                flat
                class="right"
                > Switch Customer
            </v-btn>
            
            <v-card>
                <!-- Tool Bar -->
                <v-toolbar color="white">
                    <v-btn icon  @click="dialog = false">
                        <v-icon>close</v-icon>
                    </v-btn>
                <v-spacer></v-spacer>
                <v-toolbar-items>
                    <v-btn  flat color="primary" @click="dialog = false">Submit</v-btn>
                </v-toolbar-items>
                </v-toolbar>
                <!-- Content: Outlet List -->
                <v-container grid-list-lg fluid>
                    <v-layout justify-space-around>
                        <customer-list></customer-list>
                    </v-layout>
                </v-container>
            </v-card>
            </v-dialog>
        </v-flex>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
    props:{
    },
    data () {
        return {
        dialog: false,
        }
    },
    // computed: {
    //     ...mapState({
    //         servicesChoose: state => state.service.servicesChoose
    //     })
    // }
}
</script>