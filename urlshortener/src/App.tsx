import './App.css'
import { URLForm } from './components/URLForm/URLForm';
import { ShortenerProvider } from './contexts/ShortenerContext';
import { ChakraProvider } from '@chakra-ui/react';
import tomatoTheme from './themes/tomatoTheme';

function App() {
  return (
    <ChakraProvider theme={tomatoTheme}>
      <ShortenerProvider>
        <URLForm />
      </ShortenerProvider>
    </ChakraProvider>
  )
}

export default App
