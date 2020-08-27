<template>
    <v-container fluid>
        <v-layout wrap v-for="review in reviews" :key="review.index" pb-2 column>  
            <!-- <v-flex>
                <v-layout>
                    <v-flex xs3 md3
                        class="text-xs-center" 
                    >
                        <span>Anna Wong</span>
                    </v-flex>

                    <v-flex xs9 md9>
                        <v-layout >
                            <v-spacer></v-spacer>
                        <v-icon>star_border</v-icon>
                        <v-icon>star_border</v-icon>
                        <v-icon>star_border</v-icon>
                        <v-icon>star_border</v-icon>
                        <v-icon>star_border</v-icon>
                        </v-layout>
                    </v-flex>
                </v-layout>    
            </v-flex>
            <v-flex>
                
            </v-flex> -->
            <v-card>
                <v-card-title >
                    <v-layout>
                        <v-flex xs6 md3
                        >
                            <h3>Anna Wong</h3>
                        </v-flex>

                        <v-flex xs6 md9>
                            <v-layout >
                                <v-spacer></v-spacer>
                            <v-icon>star_border</v-icon>
                            <v-icon>star_border</v-icon>
                            <v-icon>star_border</v-icon>
                            <v-icon>star_border</v-icon>
                            <v-icon>star_border</v-icon>
                            </v-layout>
                        </v-flex>
                    </v-layout>  
                </v-card-title >
                <v-flex xs9 >
                    <v-layout >
                        <span>{{review.description}}
                            </span>                     
                        
                    </v-layout>
                </v-flex>
            </v-card>
        </v-layout>
    </v-container>
</template>

<script>
export default {
    data(){
        return{
            reviews:[
                {
                    name:"My",
                    description:"Nice"
                },
                {
                    name:"Bao",
                    description:"Good"
                }
            ]
        }
    }
}
</script>

