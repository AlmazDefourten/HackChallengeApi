import { createSlice } from '@reduxjs/toolkit';



//kinda reducer itself
export const userSlice = createSlice({
    name: 'user',
    initialState: {isAuth: false},
    reducers:{
        change_auth: (state, action) =>{
            state.isAuth = action.payload
        }
    }
})
//actions (kinda pulled out from userSlice)
export const {change_auth} = userSlice.actions
