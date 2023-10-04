
<template>
  <v-container>
    <v-layout column>  

    
        </v-layout>
   
     <v-flex xs12 sm12 md12>
       <v-card flat>
         <v-card-title primary-title>
          
          <StatistikkDashbord/>
          <AppNavbar/>
  
                    </v-card-title>
                </v-card>
                
            </v-flex>
          </v-container>
</template>
<script>
import StatistikkDashbord from '@/components/StatistikkDashbord.vue';
import AppNavbar from '@/components/AppNavbar.vue';


export default {
    name: 'AppDashboard',
    components:{ StatistikkDashbord,AppNavbar},
    data (){
      return{
        title : 'Test components',
        show : false,
        frontEnd : 'Test på backenden her',
        backEnd : 'Test på frontenden her'
      }
    }
  
};




</script>

<style scoped>

.v-enter-active{
 animation: bounceIn 2s;
}

.v-leave-active{
  animation: bounceIn 2s reverse;

}
@keyframes bounceIn {
  0%{
     transform: scale(0.1);
     opacity: 0;
  }
  60%{
    transform: scale(0.2);
   

  }
  100%{
   transform:(1);
   opacity:(1);
  }
}

.fade-enter-active, .fade-leave-active{
 transition: opacity 2.5s ease-out;
 
}

.fade-enter, .fade-leave-to{
  opacity: 0;
  

}
</style>