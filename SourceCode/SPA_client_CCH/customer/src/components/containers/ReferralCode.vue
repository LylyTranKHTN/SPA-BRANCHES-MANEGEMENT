<template>
    <v-container grid-list-md fluid>
        <!-- cover -->
        <v-layout wrap>
            <v-flex xs12 sm12  class="text-xs-center">
                <v-card>
                    <v-img
                          src="https://quotefancy.com/media/wallpaper/1600x900/373486-Louise-Hay-Quote-I-am-in-the-right-place-at-the-right-time-doing.jpg"
                          height="420"
                    ></v-img>
                </v-card>
            </v-flex>
        </v-layout>
        <!-- input code -->
        <v-layout justify-space-around pt-2>
            <v-flex xs8 sm4>
                <v-text-field
                    label="Referral Number (optional)"
                    v-model="referralCode"
                    outline
                    hide-details
                ></v-text-field>
            </v-flex>
        </v-layout>
        <v-layout justify-space-around>
            <v-flex xs8 sm4 class = "text-xs-center">
                <v-btn
                    block
                    color="primary"
                    @click="logIn()"
                    dark
                    fill-height
                    to="/login"
                    >
                    Continue
                </v-btn>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import API from '@/api';
export default {
    data() {
        return{
            referralCode: null
        }
    },
    methods: {
        // logIn (){
        //     API.logIn('abc', '123')
        //     .then((result) => {
        //         console.log(result)
        //     }).catch((err) => {
        //         console.log(err)
        //     });
        // }
        logIn() {
            var a = API.logIn()
            console.log(a)
        }
    }
}
</script>
