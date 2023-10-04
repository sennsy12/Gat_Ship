
<template>
  <v-container>
    <v-layout column>  

    
        </v-layout>
   
     <v-flex xs12 sm12 md12>
       <v-card flat>
         <v-card-title primary-title>

                     <TestCalendar2/>
                     <AppNavbar/>
                    </v-card-title>
                </v-card>
                
            </v-flex>
          </v-container>
</template>

<script>
import TestCalendar2 from '@/components/TestCalendar2.vue';
import AppNavbar from '@/components/AppNavbar.vue';

export default {
  methods: {
    viewSchedule() {
      window.open('@/views/viewSchedule', '_blank');
    }
  },
  name: 'AppCalendar',
  components:{ TestCalendar2,AppNavbar }
};
</script>

<style scoped>
.btn {
  background-color: transparent;
  color: #4caf50;
  border: 2px solid #4caf50;
  border-radius: 4px;
  box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.3);
  padding: 8px 16px;
  cursor: pointer;
  transition: all 0.3s ease-in-out;
}

.btn:hover {
  background-color: #4caf50;
  color: #fff;
}



</style>
