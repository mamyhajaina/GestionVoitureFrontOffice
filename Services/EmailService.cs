using System.Net.Mail;
using System.Net;

namespace GestionVoitureFrontOffice.Services
{
    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _senderEmail = "andriantsitohainamamyhajaina@gmail.com";
        private readonly string _senderPassword = "ekur xthb aaxi dwzp";

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                    client.EnableSsl = true; // Activez SSL/TLS pour sécuriser la connexion.
                    client.UseDefaultCredentials = false;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_senderEmail, "Mamy Lahatra Hajaina Andriantsitohaina"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true // Permet d'envoyer du contenu HTML dans l'email.
                    };
                    mailMessage.To.Add(recipientEmail);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");
            }
        }

        public async Task EnvoyerConfirmationDemande(string utilisateurEmail, string nomUtilisateur, string resumeDemande)
        {
            var subject = "Confirmation de votre demande";
            var body = $@"
                <p>Bonjour {nomUtilisateur},</p>
                <p>Nous avons bien reçu votre demande.</p>
                <p><strong>Résumé de votre demande :</strong></p>
                <p>{resumeDemande}</p>
                <p>Nous vous remercions de nous avoir contactés.</p>
                <p>Cordialement,<br>Votre équipe</p>";
            Console.WriteLine("body: "+ body);
            await SendEmailAsync(utilisateurEmail, subject, body);
        }

        public string GenererResumeHtml(string lieuDepart, string lieuArrivee, double tonnage, bool offreValidee, decimal? montant)
        {
            string resume = $@"
                <h3>Résumé de la Demande</h3>
                <ul>
                    <li><strong>Lieu de départ :</strong> {lieuDepart}</li>
                    <li><strong>Lieu d'arrivée :</strong> {lieuArrivee}</li>
                    <li><strong>Tonnage :</strong> {tonnage} tonnes</li>
                </ul>";
            if (offreValidee)
            {
                resume += "<p><strong>Votre offre est déjà validée.</strong></p>";
            }
            return resume;
        }


    }
}
