import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { SearchResponse } from "../../Services/Dtos/search.response";
import atb_logo from "../Utils/Atb_logo.png";
import Fozzy_shop_logo from "../Utils/Fozzy_shop_logo.png";

export default function MediaCard(props: { response: SearchResponse }) {
  function GetLogoPath(storeName: string): any {
    if (storeName == "Атб") {
      return atb_logo;
    }
    if (storeName == "Fozzy shop") {
      return Fozzy_shop_logo;
    } else {
      return "";
    }
  }

  const handleButtonClick = () => {
    window.location.href = props.response.searchUrl;
  };

  return (
    <Card sx={{ maxWidth: 345, minWidth: 345 }}>
      <CardMedia
        sx={{ height: 140 }}
        image={GetLogoPath(props.response.storeName)}
        title="green iguana"
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {props.response.storeName}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Найменша ціна: {props.response.minPrice}
          <br />
          Найбільша ціна: {props.response.maxPrice}
          <br />
          Середня ціна: {props.response.average}
        </Typography>
        <CardActions>
          <Button size="small" onClick={handleButtonClick}>
            Перейти
          </Button>
        </CardActions>
      </CardContent>
    </Card>
  );
}
