<template>
    <v-container grid-list-xs >
        <v-layout column swap>
            <!-- Customer Choose -->
            <v-flex xs12>
                    <v-container grid-list-xs>
                        <v-layout row wrap>
                            <!-- Avarta -->
                            <v-flex xs2>
                                 <v-avatar
                                    size="50px"
                                    color="red"
                                >
                    
                                </v-avatar>
                            </v-flex>
                            <!-- Infor -->
                            <v-flex xs5>
                                <v-layout column wrap>
                                    <span>{{customer1.name}}</span>
                                    <span>{{customer1.email}}</span>
                                    <span>{{customer1.phone}}</span>
                                </v-layout>
                            </v-flex>
                            <!-- Switch customer -->
                            <v-flex xs3>
                                <switch-customer></switch-customer>
                            </v-flex>
                        </v-layout>
                    </v-container>
            </v-flex>
            <v-divider></v-divider>
            <!-- Add Service -->
            <add-new-booking></add-new-booking>

        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
//import API from '@/api';
export default {
    props:{
        customer2: {
            name: "Nguyen Thanh Huy",
            email:"abc@gmail.com",
            phone:"070707070"
        }
    },
    data() {
        return{
            customer1: {
            name: "Nguyen Thanh Huy",
            email:"abc@gmail.com",
            phone:"070707070"
        }
        }
    }
}
</script>

