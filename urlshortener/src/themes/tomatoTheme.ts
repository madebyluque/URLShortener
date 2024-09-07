import { extendTheme } from '@chakra-ui/react';

const theme = extendTheme({
  styles: {
    global: () => ({
      body: {
        bg: "gray.100",
      },
    }),
  },
  colors: {
    tomato: {
      50: '#FEF2F2',
      100: '#FEE2E2',
      200: '#FECACA',
      300: '#FCA5A5',
      400: '#F97316',
      500: '#F56565', // Base Tomato Color
      600: '#C53030', // Dark Tomato
      700: '#9B2C2C',
      800: '#742A2A',
      900: '#521D1D',
    },
    coral: {
      50: '#FFF5F0',
      100: '#FED7D7',
      200: '#FBB6CE',
      300: '#F687B3',
      400: '#F56565',
      500: '#FBD38D', // Soft Coral
      600: '#F6AD55',
      700: '#ED8936',
      800: '#DD6B20',
      900: '#C53030',
    },
    gray: {
      50: '#F7FAFC', // Warm Gray
      100: '#EDF2F7',
      200: '#E2E8F0',
      300: '#CBD5E0',
      400: '#A0AEC0',
      500: '#718096',
      600: '#4A5568',
      700: '#2D3748', // Charcoal
      800: '#1A202C',
      900: '#171923',
    },
  },
  components: {
    Button: {
      baseStyle: {
        fontWeight: 'bold',
        borderRadius: 'md',
      },
      variants: {
        solid: {
          bg: 'tomato.500',
          color: 'white',
          _hover: {
            bg: 'tomato.600',
          },
        },
        outline: {
          borderColor: 'tomato.500',
          color: 'tomato.500',
          _hover: {
            bg: 'tomato.100',
          },
        },
      },
    },
    Text: {
      baseStyle: {
        color: 'tomato.500',
      },
    },
    Heading: {
      baseStyle: {
        color: 'tomato.500'
      }
    },
    FormLabel: {
      baseStyle: {
        color: 'tomato.500',
      },
    },
    Divider: {
      baseStyle: {
        borderColor: 'tomato.500',
        borderWidth: "4px"
      }
    }
  },
});

export default theme;