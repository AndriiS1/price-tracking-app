import tokenService from "../../Services/token.service";
import "./urlsTable.component.style.css";

export default function UrlsTable() {
  const userIsLogged = tokenService.isUserLogged();

  return (
    <div className="wrap">
      <div className="welcome-content">
        <section className="">
          <h1>Welcome to price tracking app</h1>
          Sign up or login to get full functional.
        </section>
      </div>
    </div>
  );
}
