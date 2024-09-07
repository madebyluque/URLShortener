import { zodResolver } from "@hookform/resolvers/zod";
import { Send } from "lucide-react";
import { useForm } from "react-hook-form";
import { shortenUrlSchema, ShortenUrlSchema } from "../../types/shortenLink.types";
import { ShortenerContext } from "../../contexts/ShortenerContext";
import { useContext } from "react";
import {
  Button,
  Card,
  CardBody,
  CardHeader,
  Divider,
  FormControl,
  FormLabel,
  Heading,
  Text,
  Icon,
  Input,
  InputGroup,
  InputRightElement,
  Stack,
  useBreakpointValue,
} from "@chakra-ui/react";
import { Header } from "../Header/Header";

export const URLForm = () => {
  const { shortenUrl, isSubmitting, shortenedUrl } = useContext(ShortenerContext);
  const { register, handleSubmit, formState: { errors } } = useForm<ShortenUrlSchema>({
    resolver: zodResolver(shortenUrlSchema)
  });

  const handleURLSubmit = async (data: ShortenUrlSchema) => {
    const shortenLinkCommand = shortenUrlSchema.parse(data);
    await shortenUrl(shortenLinkCommand, errors);
  };

  const hasShortenedUrl = shortenedUrl !== null && shortenedUrl !== ''
  const formWidth = useBreakpointValue({ base: "90%", md: "60%", lg: "50%" });

  return (
    <Card
      marginTop="10"
      align="center"
      minWidth={formWidth}
      padding="6"
      variant="elevated"
      borderRadius="md"
      boxShadow="md"
    >
      <Header />
      <CardHeader>
        <Heading size="lg" color="tomato.300" textAlign="center">
          A simple dockerized url shortener
        </Heading>
      </CardHeader>
      <Divider margin="4"/>
      <CardBody>
        <Stack spacing={8}>
          <FormControl as="form" onSubmit={handleSubmit(handleURLSubmit)}>
            <FormLabel>Insert a link below:</FormLabel>
            <InputGroup size="lg">
              <Input
                minW="30vw"
                type="url"
                placeholder="Ex: https://google.com"
                {...register('address')}
                borderColor="gray.300"
                focusBorderColor="tomato.500"
              />
              <InputRightElement>
                <Button
                  colorScheme="tomato"
                  isLoading={isSubmitting}
                  type="submit"
                  size="lg">
                  <Icon as={Send} boxSize={5} strokeWidth={2}/>
                </Button>
              </InputRightElement>
            </InputGroup>
          </FormControl>
          {hasShortenedUrl && (
            <Heading lineHeight='tall' size='md' justifySelf='right'>
                Short URL: &nbsp;
                <Text as="span" px="2" py="1" rounded="full" bg="red.100" padding={3}>
                  <a href={shortenedUrl} target="_blank">{shortenedUrl}</a>
                </Text>
            </Heading>
          )}
        </Stack>
      </CardBody>
    </Card>
  );
}