<template>
        <v-dialog
            v-model="dialog"
            scrollable fullscreen
            persistent :overlay="false"
            max-width="500px"
            transition="dialog-transition"
        >
             <v-btn
                slot="activator"
                color="info"
                dark
                flat
            >
                Term and Conditions
            </v-btn>
            <v-card>
                <v-card-text>
                    <h2 class="text-xs-center">Term and Conditions</h2>
                </v-card-text>

                <v-card-text>
                    <p>In your using app times, you cannot use your hand.</p>
                    <p>Content of Term and condition</p>
                    <p>Thank you very much! Love you! </p>
                </v-card-text>

                <p class="text-xs-center">
                    <v-btn @click="dialog = false" color="info">Back to Register</v-btn>
                </p>

                <v-card-text>
                    <p class="text-xs-center">Login or register for social media</p>
                </v-card-text>
                </v-card>
        </v-dialog>
</template>
<script>
export default {
    data () {
        return{
            dialog: false
        }
    }
}
</script>
