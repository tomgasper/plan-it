import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { Workspace } from '../types/Project'

interface WorkspacesState {
  workspaces: Workspace[]
}

const initialState: WorkspacesState = {
  workspaces: []
}

const workspacesSlice = createSlice({
  name: 'workspaces',
  initialState,
  reducers: {
    addWorkspace: (state, action: PayloadAction<Workspace>) => {
      state.workspaces.push(action.payload)
    },
    updateWorkspaceLocal: (state, action: PayloadAction<Workspace>) => {
      const index = state.workspaces.findIndex(workspace => workspace.id === action.payload.id)
      if (index !== -1) {
        state.workspaces[index] = action.payload
      }
    },
    deleteWorkspaceLocal: (state, action: PayloadAction<string>) => {
      state.workspaces = state.workspaces.filter(workspace => workspace.id !== action.payload)
    },
    setWorkspaces: (state, action: PayloadAction<Workspace[]>) => {
      state.workspaces = action.payload
    }
  }
})

export const {
  addWorkspace,
  updateWorkspaceLocal,
  deleteWorkspaceLocal,
  setWorkspaces
 } = workspacesSlice.actions
export default workspacesSlice.reducer
