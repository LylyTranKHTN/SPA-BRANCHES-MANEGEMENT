<template>
    <v-container grid-list-xs>
        <v-layout row wrap justify-center>
            <v-flex xs10 sm8 md8>
                <v-select
                    v-model="currentOutlet"
                    label="Choose Outlet"
                    :items="outlets"
                    item-value="id"
                    item-text="name"
                    return-object
                    outline
                ></v-select>
            </v-flex>
        </v-layout>
        <v-layout row wrap justify-center="">
            <v-flex xs5 px-1 v-for="item in items" :key="item.index">
                <v-btn
                    :to="item.link"
                    block
                    color="success">
                {{item.title}}
                </v-btn>
            </v-flex>
        </v-layout>
    </v-container>
</template>
<script>
import { mapState, mapActions } from 'vuex'
export default {
    data (){
        return{
            items: [
                {title: 'User Manager', link: './users'},
                {title: 'Outlet Manager',link: './outlets'},
                {title: 'Service Manager', link: './services'},
                {title: 'Room Manager', link: './rooms'},
                {title: 'Bed Manager', link: './beds'}
            ],
            // outlets: ['1', '2', '3', '4']
        }
    },
    created(){
        this.loadAllOutlets()
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
        }),
        currentOutlet: {
            get() {
                return this.$store.state.outlet.outlet
            },
            set(newOutlet) {
                this.$store.dispatch('outlet/changeIDCurrentOutLet', newOutlet)
            }
        },
    },
    // for function for event
    methods: {
        loadAllOutlets(){
            this.$store.dispatch('outlet/getAllOutlet')
        },
    }
}
</script>
