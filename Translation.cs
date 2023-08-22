using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildItModsSelector
{
    public partial class Translation : Form
    {
        private string version = "1.1pre-release-1";
        private int app_build = 10;

        public Translation()
        {
            InitializeComponent();
        }

        public string getLanguageCode()
        {
            string ProfileFilePath = Path.Combine("sys", "profile.txt");
            string[] lines = File.ReadAllLines(ProfileFilePath);
            string code = "EN";

            foreach (string line in lines)
            {
                if (line.StartsWith("language="))
                {
                    string language = line.Substring(line.IndexOf('=') + 1);
                    Console.WriteLine($"Current language: {language}");
                    return language;
                }
            }
            return code;
        }

        private Dictionary<string, string> getContributor()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            return keyValuePairs;
        }

        private string getContributorsNamesAndLbl()
        {
            string res = string.Empty;
            return res;
        }

        public string translatedText(string textCode)
        {
            string resultat = "MISSING TRANSLATION";
            switch (getLanguageCode())
            {
                // french
                case "FR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Une erreur s'est produite :( attention ce logiciel doit être placé dans le dossier mods de votre .minecraft !\nVerifiez aussi que vous avez bien téléchargé les mods !"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifiez votre connexion."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Cette feature n'est pas encore implémentée"; break;
                        case "ERR_EXEC-BATCH": resultat = "Une erreur s'est produite lors de l'exécution du fichier batch : "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Il semblerait qu'il soit pas possible de lancer minecraft"; break;
                        case "ERR_NOITEMS_SELECTED": resultat = "Vous n'avez selectionné aucun item, l'action a été ignoré"; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE": resultat = "Cette fonctionalité n'a pas encore été implementée."; break;
                        case "ERR_NOTIMPLEMENTED_FEATURE_VERSION": resultat = "Cette fonctionalité n'a pas encore été implementée. Cette feature sera disponible en version "; break;

                        case "WARN_WRONGFOLDER": resultat = "Mauvais dossier : "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Le logiciel doit être exécuté depuis le dossier 'mods' de votre .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Regarde ce que ça fait..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Ouvrir le rapport de problème."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Ouvre l'URL du changelog, résumé des changement par mises à jour."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Ouvre le repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Ouvre l'URL pour télécharger Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Télécharger / Mettre à jour les mods"; break;

                        case "INFO_SUCCESS": resultat = "Tout s'est déroulé avec succés !"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion :{version}\nBuild : {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Je suis MythMega (j'ai pas encore crée cette section, c'est moins urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Les contributeurs sont les suivants :\n{getContributorsNamesAndLbl()}\nMerci à eux !"; break;

                        case "CONTRIBUTORS": resultat = "Contributeurs"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduction"; break;
                        case "CONTRIB_DEV": resultat = "Developpement"; break;
                        case "CONTRIB_ARTIST": resultat = "Artiste/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testeur"; break;
                        case "CONTRIB_OTHER": resultat = "Autre"; break;

                        case "LBL_WARNING": resultat = "Attention !"; break;
                        case "LBL_CONFIRMATION": resultat = "ëtes vous sur de vouloir faire cette action ?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tout sélectionner"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tout déselctionner"; break;
                        case "LBL_STOP": resultat = "Arrêter"; break;
                        case "LBL_OUI": resultat = "Oui"; break;
                        case "LBL_NON": resultat = "Non"; break;
                        case "LBL_ANNULER": resultat = "Annuler"; break;
                        case "LBL_LOCALISER": resultat = "Localiser"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // English
                case "EN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "An error occurred :( Please ensure this software is placed in the 'mods' folder of your .minecraft!\nAlso, make sure you have downloaded the mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Check your connection."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "This feature is not yet implemented."; break;
                        case "ERR_EXEC-BATCH": resultat = "An error occurred while executing the batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "It seems that launching Minecraft is not possible."; break;

                        case "WARN_WRONGFOLDER": resultat = "Wrong folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "The software must be executed from the 'mods' folder of your .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "See what it does..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Open problem report."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Open the changelog URL, summary of changes per updates."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Open the GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Open the URL to download Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Download / Update mods"; break;

                        case "INFO_SUCCESS": resultat = "Everything went successfully!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nby MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "I am MythMega (I haven't created this section yet, it's less urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"The contributors are as follows:\n{getContributorsNamesAndLbl()}\nThanks to them!"; break;

                        case "CONTRIBUTORS": resultat = "Contributors"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Translation"; break;
                        case "CONTRIB_DEV": resultat = "Development"; break;
                        case "CONTRIB_ARTIST": resultat = "Artist/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Other"; break;

                        case "LBL_WARNING": resultat = "Warning!"; break;
                        case "LBL_CONFIRMATION": resultat = "Are you sure you want to perform this action?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deselect All"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Select All"; break;
                        case "LBL_STOP": resultat = "Stop"; break;
                        case "LBL_OUI": resultat = "Yes"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancel"; break;
                        case "LBL_LOCALISER": resultat = "Locate"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Spanish
                case "ES":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "¡Se ha producido un error! :( Asegúrate de que este software esté ubicado en la carpeta 'mods' de tu .minecraft.\n¡También verifica que hayas descargado los mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Tiempo agotado - Verifica tu conexión."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta característica aún no está implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Se ha producido un error al ejecutar el archivo por lotes: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que no es posible ejecutar Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Carpeta incorrecta: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "El software debe ejecutarse desde la carpeta 'mods' de tu .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Observa qué hace..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir el informe del problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir la URL del registro de cambios, resumen de los cambios por actualizaciones."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir el repositorio de GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir la URL para descargar Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Descargar / Actualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "¡Todo se realizó con éxito!"; break;
                        case "INFO_ABOUT": resultat = $"Mis Mods de Supervivencia\npor MythMega\n2023\nVersión: {version}\nCompilación: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Soy MythMega (todavía no he creado esta sección, es menos urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Los colaboradores son los siguientes:\n{getContributorsNamesAndLbl()}\n¡Gracias a ellos!"; break;

                        case "CONTRIBUTORS": resultat = "Colaboradores"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traducción"; break;
                        case "CONTRIB_DEV": resultat = "Desarrollo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Diseño"; break;
                        case "CONTRIB_TESTER": resultat = "Probador"; break;
                        case "CONTRIB_OTHER": resultat = "Otro"; break;

                        case "LBL_WARNING": resultat = "¡Advertencia!"; break;
                        case "LBL_CONFIRMATION": resultat = "¿Estás seguro de que quieres realizar esta acción?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deseleccionar Todo"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Seleccionar Todo"; break;
                        case "LBL_STOP": resultat = "Detener"; break;
                        case "LBL_OUI": resultat = "Sí"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // German
                case "DE":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ein Fehler ist aufgetreten :( Stelle sicher, dass diese Software im 'mods'-Ordner deines .minecraft-Verzeichnisses platziert ist!\nStelle außerdem sicher, dass du die Mods heruntergeladen hast!"; break;
                        case "ERR_TIMEOUT": resultat = "Zeitüberschreitung - Überprüfe deine Verbindung."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Diese Funktion ist noch nicht implementiert."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ein Fehler ist während der Ausführung der Stapeldatei aufgetreten: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Es scheint, dass das Starten von Minecraft nicht möglich ist."; break;

                        case "WARN_WRONGFOLDER": resultat = "Falscher Ordner: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Die Software muss aus dem 'mods'-Ordner deines .minecraft-Verzeichnisses ausgeführt werden."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Schau, was passiert..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Problembericht öffnen."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Öffne die URL des Changelogs, Zusammenfassung der Änderungen pro Update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub-Repository öffnen."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Öffne die URL zum Herunterladen von Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Mods herunterladen / aktualisieren"; break;

                        case "INFO_SUCCESS": resultat = "Alles lief erfolgreich ab!"; break;
                        case "INFO_ABOUT": resultat = $"Meine Survival Mods\nvon MythMega\n2023\nVersion: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ich bin MythMega (ich habe diesen Abschnitt noch nicht erstellt, es ist weniger dringend)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Die Beitragenden sind wie folgt:\n{getContributorsNamesAndLbl()}\nDanke an sie!"; break;

                        case "CONTRIBUTORS": resultat = "Beitragende"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Übersetzung"; break;
                        case "CONTRIB_DEV": resultat = "Entwicklung"; break;
                        case "CONTRIB_ARTIST": resultat = "Künstler/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Andere"; break;

                        case "LBL_WARNING": resultat = "Warnung!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bist du sicher, dass du diese Aktion durchführen möchtest?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Alles abwählen"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Alles auswählen"; break;
                        case "LBL_STOP": resultat = "Stoppen"; break;
                        case "LBL_OUI": resultat = "Ja"; break;
                        case "LBL_NON": resultat = "Nein"; break;
                        case "LBL_ANNULER": resultat = "Abbrechen"; break;
                        case "LBL_LOCALISER": resultat = "Lokalisieren"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Italian
                case "IT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Si è verificato un errore :( Assicurati che questo software sia posizionato nella cartella 'mods' del tuo .minecraft!\nVerifica anche di aver scaricato le mod!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifica la tua connessione."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Questa funzionalità non è ancora implementata."; break;
                        case "ERR_EXEC-BATCH": resultat = "Si è verificato un errore durante l'esecuzione del file batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sembra che non sia possibile avviare Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Cartella errata: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Il software deve essere eseguito dalla cartella 'mods' del tuo .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Guarda cosa fa..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Apri il rapporto del problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Apri l'URL del registro modifiche, riepilogo delle modifiche per aggiornamento."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Apri il repository GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Apri l'URL per scaricare Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Scarica / Aggiorna le mod"; break;

                        case "INFO_SUCCESS": resultat = "Tutto è andato bene con successo!"; break;
                        case "INFO_ABOUT": resultat = $"Le Mie Mod per la Sopravvivenza\ndi MythMega\n2023\nVersione: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Sono MythMega (non ho ancora creato questa sezione, è meno urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"I collaboratori sono i seguenti:\n{getContributorsNamesAndLbl()}\nGrazie a loro!"; break;

                        case "CONTRIBUTORS": resultat = "Collaboratori"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Traduzione"; break;
                        case "CONTRIB_DEV": resultat = "Sviluppo"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Tester"; break;
                        case "CONTRIB_OTHER": resultat = "Altro"; break;

                        case "LBL_WARNING": resultat = "Attenzione!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sei sicuro di voler eseguire questa azione?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Deseleziona tutto"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Seleziona tutto"; break;
                        case "LBL_STOP": resultat = "Ferma"; break;
                        case "LBL_OUI": resultat = "Sì"; break;
                        case "LBL_NON": resultat = "No"; break;
                        case "LBL_ANNULER": resultat = "Annulla"; break;
                        case "LBL_LOCALISER": resultat = "Trova"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Portuguese
                case "PT":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Ocorreu um erro :( Certifique-se de que este software está na pasta 'mods' do seu .minecraft!\nCertifique-se também de que baixou as mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Verifique a sua conexão."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Esta funcionalidade ainda não foi implementada."; break;
                        case "ERR_EXEC-BATCH": resultat = "Ocorreu um erro durante a execução do arquivo em lote: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Parece que não é possível iniciar o Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Pasta incorreta: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "O software deve ser executado a partir da pasta 'mods' do seu .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Veja o que faz..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Abrir relatório de problemas."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Abrir URL do changelog, resumo das mudanças por atualização."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Abrir repositório GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Abrir URL para baixar o Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Baixar / Atualizar mods"; break;

                        case "INFO_SUCCESS": resultat = "Tudo correu com sucesso!"; break;
                        case "INFO_ABOUT": resultat = $"As Minhas Mods de Sobrevivência\npor MythMega\n2023\nVersão: {version}\nCompilação: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Eu sou o MythMega (ainda não criei esta seção, é menos urgente)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Os contribuidores são os seguintes:\n{getContributorsNamesAndLbl()}\nAgradecemos a eles!"; break;

                        case "CONTRIBUTORS": resultat = "Contribuidores"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Tradução"; break;
                        case "CONTRIB_DEV": resultat = "Desenvolvimento"; break;
                        case "CONTRIB_ARTIST": resultat = "Artista/Design"; break;
                        case "CONTRIB_TESTER": resultat = "Testador"; break;
                        case "CONTRIB_OTHER": resultat = "Outro"; break;

                        case "LBL_WARNING": resultat = "Aviso!"; break;
                        case "LBL_CONFIRMATION": resultat = "Tem certeza de que deseja realizar esta ação?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Desselecionar Tudo"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Selecionar Tudo"; break;
                        case "LBL_STOP": resultat = "Parar"; break;
                        case "LBL_OUI": resultat = "Sim"; break;
                        case "LBL_NON": resultat = "Não"; break;
                        case "LBL_ANNULER": resultat = "Cancelar"; break;
                        case "LBL_LOCALISER": resultat = "Localizar"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Arabic
                case "AR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "حدث خطأ :( يُرجى التأكد من وضع هذا البرنامج في مجلد 'mods' في ملف .minecraft الخاص بك!\nتأكد أيضًا من تنزيل المودات!"; break;
                        case "ERR_TIMEOUT": resultat = "مهلة زمنية - تحقق من اتصالك."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "هذه الميزة لم تُنفذ بعد."; break;
                        case "ERR_EXEC-BATCH": resultat = "حدث خطأ أثناء تنفيذ ملف الدفع: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "يبدو أنه من غير الممكن تشغيل ماين كرافت."; break;

                        case "WARN_WRONGFOLDER": resultat = "مجلد غير صحيح: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "يجب تشغيل البرنامج من مجلد 'mods' في ملف .minecraft الخاص بك."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "انظر ماذا يحدث..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "فتح تقرير المشكلة."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "فتح عنوان URL لسجل التغييرات، ملخص للتغييرات حسب التحديثات."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "فتح مستودع GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "فتح العنوان URL لتنزيل Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "تنزيل / تحديث المودات"; break;

                        case "INFO_SUCCESS": resultat = "كل شيء تم بنجاح!"; break;
                        case "INFO_ABOUT": resultat = $"مودات البقاء الخاصة بي\nبواسطة MythMega\n2023\nالإصدار: {version}\nالبناء: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "أنا MythMega (لم أقم بإنشاء هذا القسم بعد، أمر أقل ضرورة)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"المساهمون هم على النحو التالي:\n{getContributorsNamesAndLbl()}\nشكراً لهم!"; break;

                        case "CONTRIBUTORS": resultat = "المساهمون"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "الترجمة"; break;
                        case "CONTRIB_DEV": resultat = "التطوير"; break;
                        case "CONTRIB_ARTIST": resultat = "الفنان/التصميم"; break;
                        case "CONTRIB_TESTER": resultat = "المُختبر"; break;
                        case "CONTRIB_OTHER": resultat = "آخر"; break;

                        case "LBL_WARNING": resultat = "تحذير!"; break;
                        case "LBL_CONFIRMATION": resultat = "هل أنت متأكد من رغبتك في القيام بهذا الإجراء؟"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "إلغاء تحديد الكل"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "تحديد الكل"; break;
                        case "LBL_STOP": resultat = "إيقاف"; break;
                        case "LBL_OUI": resultat = "نعم"; break;
                        case "LBL_NON": resultat = "لا"; break;
                        case "LBL_ANNULER": resultat = "إلغاء"; break;
                        case "LBL_LOCALISER": resultat = "تحديد الموقع"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Russian
                case "RU":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Произошла ошибка :( Пожалуйста, убедитесь, что это программное обеспечение находится в папке 'mods' вашей .minecraft!\nТакже убедитесь, что вы загрузили моды!"; break;
                        case "ERR_TIMEOUT": resultat = "Тайм-аут - Проверьте ваше соединение."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Эта функция еще не реализована."; break;
                        case "ERR_EXEC-BATCH": resultat = "Произошла ошибка при выполнении пакетного файла: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Похоже, что запуск Minecraft невозможен."; break;

                        case "WARN_WRONGFOLDER": resultat = "Неправильная папка: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Программу следует запускать из папки 'mods' вашей .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Посмотрите, что это делает..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Открыть отчет о проблеме."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Открыть URL-адрес изменений, сводка изменений по обновлениям."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Открыть репозиторий GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Открыть URL-адрес для загрузки Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Скачать / Обновить моды"; break;

                        case "INFO_SUCCESS": resultat = "Все прошло успешно!"; break;
                        case "INFO_ABOUT": resultat = $"Мои Моды Выживания\nот MythMega\n2023\nВерсия: {version}\nСборка: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Я MythMega (я еще не создал этот раздел, это менее срочно)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Следующие люди внесли вклад:\n{getContributorsNamesAndLbl()}\nСпасибо им!"; break;

                        case "CONTRIBUTORS": resultat = "Участники"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Переводчик"; break;
                        case "CONTRIB_DEV": resultat = "Разработка"; break;
                        case "CONTRIB_ARTIST": resultat = "Художник/Дизайнер"; break;
                        case "CONTRIB_TESTER": resultat = "Тестировщик"; break;
                        case "CONTRIB_OTHER": resultat = "Другое"; break;

                        case "LBL_WARNING": resultat = "Внимание!"; break;
                        case "LBL_CONFIRMATION": resultat = "Вы уверены, что хотите выполнить это действие?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Снять все выделение"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Выделить все"; break;
                        case "LBL_STOP": resultat = "Остановить"; break;
                        case "LBL_OUI": resultat = "Да"; break;
                        case "LBL_NON": resultat = "Нет"; break;
                        case "LBL_ANNULER": resultat = "Отмена"; break;
                        case "LBL_LOCALISER": resultat = "Найти"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Tagalog
                case "TL":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "May naganap na error :( Tiyaking nasa 'mods' na folder ng iyong .minecraft ang software na ito!\nSiguruhin din na na-download mo ang mga mods!"; break;
                        case "ERR_TIMEOUT": resultat = "Timeout - Suriin ang iyong koneksyon."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Ang feature na ito ay hindi pa naipapatupad."; break;
                        case "ERR_EXEC-BATCH": resultat = "May naganap na error habang inii-execute ang batch file: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Mukhang hindi maaring buksan ang Minecraft."; break;

                        case "WARN_WRONGFOLDER": resultat = "Maling folder: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Ang software ay dapat na ma-eexecute mula sa 'mods' na folder ng iyong .minecraft."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Tingnan kung ano ang nagaganap..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buksan ang ulat ng problema."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buksan ang changelog URL, buod ng mga pagbabago sa bawat update."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buksan ang GitHub repository."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buksan ang URL para sa pag-download ng Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "I-download / I-update ang mga mods"; break;

                        case "INFO_SUCCESS": resultat = "Lahat ay naging matagumpay!"; break;
                        case "INFO_ABOUT": resultat = $"Aking Mga Mods sa Survival\nni MythMega\n2023\nBersyon: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ako si MythMega (Hindi ko pa nabuo ang seksyon na ito, hindi pa ito urgent)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Ang mga kontribyutor ay sumusunod:\n{getContributorsNamesAndLbl()}\nSalamat sa kanila!"; break;

                        case "CONTRIBUTORS": resultat = "Mga Kontribyutor"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Pagsasalin"; break;
                        case "CONTRIB_DEV": resultat = "Pag-develop"; break;
                        case "CONTRIB_ARTIST": resultat = "Artist/Disenyo"; break;
                        case "CONTRIB_TESTER": resultat = "Tagatasa"; break;
                        case "CONTRIB_OTHER": resultat = "Iba pa"; break;

                        case "LBL_WARNING": resultat = "Babala!"; break;
                        case "LBL_CONFIRMATION": resultat = "Sigurado ka bang nais mong gawin ang aksyong ito?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Huwag Piliin Lahat"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Piliin Lahat"; break;
                        case "LBL_STOP": resultat = "Itigil"; break;
                        case "LBL_OUI": resultat = "Oo"; break;
                        case "LBL_NON": resultat = "Hindi"; break;
                        case "LBL_ANNULER": resultat = "Kanselahin"; break;
                        case "LBL_LOCALISER": resultat = "Hanapin"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Hindi
                case "HI":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "कुछ त्रुटि हुई है :( कृपया सुनिश्चित करें कि यह सॉफ़्टवेयर आपके .minecraft के 'mods' फ़ोल्डर में है!\nयह भी सुनिश्चित करें कि आपने मॉड डाउनलोड किए हैं!"; break;
                        case "ERR_TIMEOUT": resultat = "टाइमआउट - अपना कनेक्शन जांचें।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "यह सुविधा अभी तक लागू नहीं की गई है।"; break;
                        case "ERR_EXEC-BATCH": resultat = "बैच फ़ाइल को निष्पादित करते समय कुछ त्रुटि हुई: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "लगता है कि Minecraft चलाना संभावित नहीं है।"; break;

                        case "WARN_WRONGFOLDER": resultat = "गलत फ़ोल्डर: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "सॉफ़्टवेयर को आपके .minecraft के 'mods' फ़ोल्डर से चलाया जाना चाहिए।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "देखें कि यह क्या करता है..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "समस्या रिपोर्ट खोलें।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "बदलाव URL खोलें, अपडेट्स के लिए परिवर्तनों का संक्षेप।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub रिपॉजिटरी खोलें।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric डाउनलोड करने के लिए URL खोलें।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "मॉड डाउनलोड / अपडेट करें"; break;

                        case "INFO_SUCCESS": resultat = "सब कुछ सफलतापूर्वक हुआ!"; break;
                        case "INFO_ABOUT": resultat = $"मेरे Survival Mods\nनिर्माता: MythMega\n2023\nसंस्करण: {version}\nबिल्ड: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "मैं MythMega हूँ (मैंने इस खंड को अभी तक नहीं बनाया है, यह जरूरी नहीं है)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"कोन्ट्रिब्यूटर्स निम्नलिखित हैं:\n{getContributorsNamesAndLbl()}\nउन्हें धन्यवाद!"; break;

                        case "CONTRIBUTORS": resultat = "कोन्ट्रिब्यूटर्स"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "अनुवादक"; break;
                        case "CONTRIB_DEV": resultat = "डेवलपमेंट"; break;
                        case "CONTRIB_ARTIST": resultat = "कलाकार/डिज़ाइन"; break;
                        case "CONTRIB_TESTER": resultat = "टेस्टर"; break;
                        case "CONTRIB_OTHER": resultat = "अन्य"; break;

                        case "LBL_WARNING": resultat = "चेतावनी!"; break;
                        case "LBL_CONFIRMATION": resultat = "क्या आप वाकई इस क्रिया को करना चाहते हैं?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "सभी को अचयन करें"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "सभी को चयनित करें"; break;
                        case "LBL_STOP": resultat = "रोकें"; break;
                        case "LBL_OUI": resultat = "हाँ"; break;
                        case "LBL_NON": resultat = "नहीं"; break;
                        case "LBL_ANNULER": resultat = "रद्द करें"; break;
                        case "LBL_LOCALISER": resultat = "स्थानित करें"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Bengali
                case "BN":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "একটি ত্রুটি ঘটেছে :( দয়া করে নিশ্চিত করুন যে এই সফটওয়্যারটি আপনার .minecraft ফোল্ডারের 'mods' ফোল্ডারে রয়েছে!\nএছাড়া, আপনি মডগুলি ডাউনলোড করেছেন তা নিশ্চিত করুন!"; break;
                        case "ERR_TIMEOUT": resultat = "টাইমআউট - আপনার সংযোগ পরীক্ষা করুন।"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "এই বৈশিষ্ট্যটি এখনও সম্পন্ন হয়নি।"; break;
                        case "ERR_EXEC-BATCH": resultat = "ব্যাচ ফাইল নিষ্পাদন করার সময় একটি ত্রুটি ঘটেছে: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "মাইনক্রাফট চালাতে সম্ভব হচ্ছে না।"; break;

                        case "WARN_WRONGFOLDER": resultat = "ভুল ফোল্ডার: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "সফটওয়্যারটি আপনার .minecraft এর 'mods' ফোল্ডার থেকে চালানো আবশ্যক।"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "এটি কী করে দেখুন..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "সমস্যা রিপোর্ট খুলুন।"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "চেঞ্জলগ URL খুলুন, আপডেটগুলির সারাংশ দেখানো হয়।"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub রিপোজিটরি খুলুন।"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "ফ্যাব্রিক ডাউনলোড করার জন্য URL খুলুন।"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "মডগুলি ডাউনলোড / আপডেট করুন"; break;

                        case "INFO_SUCCESS": resultat = "সব সফলভাবে গতিবদ্ধ হয়েছে!"; break;
                        case "INFO_ABOUT": resultat = $"আমার সার্ভাইভাল মডস\nলেখক: MythMega\n২০২৩\nসংস্করণ: {version}\nবিল্ড: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "আমি মিথমেগা (আমি এই বিভাগটি এখনও তৈরি করনি, এটি তাড়াতাড়ি প্রয়োজন নেই)।"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"যারা অবদান দিয়েছেন তাদের তালিকা নিম্নলিখিত:\n{getContributorsNamesAndLbl()}\nতাদের জন্য ধন্যবাদ!"; break;

                        case "CONTRIBUTORS": resultat = "অবদানকারীগণ"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "অনুবাদক"; break;
                        case "CONTRIB_DEV": resultat = "ডেভলপমেন্ট"; break;
                        case "CONTRIB_ARTIST": resultat = "শিল্পী/ডিজাইন"; break;
                        case "CONTRIB_TESTER": resultat = "টেস্টার"; break;
                        case "CONTRIB_OTHER": resultat = "অন্যান্য"; break;

                        case "LBL_WARNING": resultat = "সতর্কবার্তা!"; break;
                        case "LBL_CONFIRMATION": resultat = "আপনি কি নিশ্চিত যে আপনি এই ক্রিয়া পুনরায় চালাতে চান?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "সব অপশন বাদ দিন"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "সব চিহ্নিত করুন"; break;
                        case "LBL_STOP": resultat = "বন্ধ করুন"; break;
                        case "LBL_OUI": resultat = "হাঁ"; break;
                        case "LBL_NON": resultat = "না"; break;
                        case "LBL_ANNULER": resultat = "বাতিল করুন"; break;
                        case "LBL_LOCALISER": resultat = "স্থান নির্ধারণ করুন"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Japanese
                case "JA":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "エラーが発生しました :( このソフトウェアが .minecraft の 'mods' フォルダに配置されていることを確認してください！\nまた、モッズをダウンロードしていることを確認してください！"; break;
                        case "ERR_TIMEOUT": resultat = "タイムアウト - 接続を確認してください。"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "この機能はまだ実装されていません。"; break;
                        case "ERR_EXEC-BATCH": resultat = "バッチファイルを実行する際にエラーが発生しました: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft を起動することはできないようです。"; break;

                        case "WARN_WRONGFOLDER": resultat = "間違ったフォルダ："; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "このソフトウェアは .minecraft の 'mods' フォルダから実行する必要があります。"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "何をするかを見てみてください..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "問題報告を開く。"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "変更ログの URL を開く、アップデートごとの変更の概要。"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub リポジトリを開く。"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric をダウンロードするための URL を開く。"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "モッズをダウンロード / 更新"; break;

                        case "INFO_SUCCESS": resultat = "すべて正常に完了しました！"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\n作成者：MythMega\n2023\nバージョン：{version}\nビルド：{app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "私は MythMega です（このセクションはまだ作成していません、緊急性は低いです）。"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"コントリビューターは次のとおりです：\n{getContributorsNamesAndLbl()}\n彼らに感謝！"; break;

                        case "CONTRIBUTORS": resultat = "コントリビューター"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "翻訳者"; break;
                        case "CONTRIB_DEV": resultat = "開発者"; break;
                        case "CONTRIB_ARTIST": resultat = "アーティスト/デザイン"; break;
                        case "CONTRIB_TESTER": resultat = "テスター"; break;
                        case "CONTRIB_OTHER": resultat = "その他"; break;

                        case "LBL_WARNING": resultat = "警告！"; break;
                        case "LBL_CONFIRMATION": resultat = "このアクションを実行してもよろしいですか？"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "すべて選択解除"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "すべて選択"; break;
                        case "LBL_STOP": resultat = "停止"; break;
                        case "LBL_OUI": resultat = "はい"; break;
                        case "LBL_NON": resultat = "いいえ"; break;
                        case "LBL_ANNULER": resultat = "キャンセル"; break;
                        case "LBL_LOCALISER": resultat = "位置を特定"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Korean
                case "KO":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "오류가 발생했습니다 :( 이 소프트웨어를 .minecraft의 'mods' 폴더에 배치하십시오!\n또한 모드를 다운로드했는지 확인하십시오!"; break;
                        case "ERR_TIMEOUT": resultat = "타임아웃 - 연결을 확인하십시오."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "이 기능은 아직 구현되지 않았습니다."; break;
                        case "ERR_EXEC-BATCH": resultat = "일괄 파일을 실행하는 동안 오류가 발생했습니다: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft을(를) 시작할 수 없는 것 같습니다."; break;

                        case "WARN_WRONGFOLDER": resultat = "잘못된 폴더: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "이 소프트웨어는 .minecraft의 'mods' 폴더에서 실행되어야 합니다."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "이게 무슨 일인지 확인해보세요..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "문제 보고서 열기."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "변경 내역 URL 열기, 업데이트별 변경 사항 요약."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub 저장소 열기."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric 다운로드용 URL 열기."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "모드 다운로드 / 업데이트"; break;

                        case "INFO_SUCCESS": resultat = "모든 작업이 성공적으로 완료되었습니다!"; break;
                        case "INFO_ABOUT": resultat = $"내 서바이벌 모드\n작성자: MythMega\n2023\n버전: {version}\n빌드: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "나는 MythMega입니다 (이 섹션을 아직 만들지 않았습니다, 긴급하지 않습니다)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"기여자 목록은 다음과 같습니다:\n{getContributorsNamesAndLbl()}\n그들에게 감사드립니다!"; break;

                        case "CONTRIBUTORS": resultat = "기여자"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "번역자"; break;
                        case "CONTRIB_DEV": resultat = "개발자"; break;
                        case "CONTRIB_ARTIST": resultat = "아티스트/디자이너"; break;
                        case "CONTRIB_TESTER": resultat = "테스터"; break;
                        case "CONTRIB_OTHER": resultat = "기타"; break;

                        case "LBL_WARNING": resultat = "경고!"; break;
                        case "LBL_CONFIRMATION": resultat = "이 작업을 수행하시겠습니까?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "모두 선택 해제"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "모두 선택"; break;
                        case "LBL_STOP": resultat = "중지"; break;
                        case "LBL_OUI": resultat = "예"; break;
                        case "LBL_NON": resultat = "아니오"; break;
                        case "LBL_ANNULER": resultat = "취소"; break;
                        case "LBL_LOCALISER": resultat = "찾기"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Turkish
                case "TR":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Bir hata oluştu :( Lütfen bu yazılımı .minecraft klasörünüzün 'mods' klasörüne yerleştirdiğinizden emin olun!\nAyrıca, modları indirdiğinizden emin olun!"; break;
                        case "ERR_TIMEOUT": resultat = "Zaman aşımı - Bağlantınızı kontrol edin."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Bu özellik henüz uygulanmadı."; break;
                        case "ERR_EXEC-BATCH": resultat = "Toplu işlem dosyası çalıştırılırken bir hata oluştu: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Minecraft'u başlatmak mümkün görünmüyor."; break;

                        case "WARN_WRONGFOLDER": resultat = "Yanlış klasör: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Yazılım, .minecraft'ınızın 'mods' klasöründen çalıştırılmalıdır."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Ne yaptığını gör..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Sorun raporunu aç."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Değişiklik günlüğü URL'sini aç, güncellemelerin özetleri."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "GitHub deposunu aç."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Fabric'i indirmek için URL'yi aç."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Modları İndir / Güncelle"; break;

                        case "INFO_SUCCESS": resultat = "Her şey başarıyla gerçekleşti!"; break;
                        case "INFO_ABOUT": resultat = $"Benim Survival Modlarım\nMythMega tarafından\n2023\nSürüm: {version}\nDerleme: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Ben MythMega'yım (Bu bölümü henüz oluşturmadım, daha az acil)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Katkıda bulunanlar aşağıdaki gibidir:\n{getContributorsNamesAndLbl()}\nOnlara teşekkürler!"; break;

                        case "CONTRIBUTORS": resultat = "Katkıda Bulunanlar"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Çevirmen"; break;
                        case "CONTRIB_DEV": resultat = "Geliştirme"; break;
                        case "CONTRIB_ARTIST": resultat = "Sanatçı/Desen"; break;
                        case "CONTRIB_TESTER": resultat = "Testçi"; break;
                        case "CONTRIB_OTHER": resultat = "Diğer"; break;

                        case "LBL_WARNING": resultat = "Uyarı!"; break;
                        case "LBL_CONFIRMATION": resultat = "Bu eylemi gerçekleştirmek istediğinizden emin misiniz?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tümünü Kaldır"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Tümünü Seç"; break;
                        case "LBL_STOP": resultat = "Durdur"; break;
                        case "LBL_OUI": resultat = "Evet"; break;
                        case "LBL_NON": resultat = "Hayır"; break;
                        case "LBL_ANNULER": resultat = "İptal"; break;
                        case "LBL_LOCALISER": resultat = "Konumlandır"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Thai
                case "TH":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "เกิดข้อผิดพลาด :( โปรดตรวจสอบว่าซอฟต์แวร์นี้อยู่ในโฟลเดอร์ 'mods' ของ .minecraft ของคุณ!\nนอกจากนี้ โปรดตรวจสอบว่าคุณได้ดาวน์โหลด mod ทั้งหมด!"; break;
                        case "ERR_TIMEOUT": resultat = "หมดเวลา - ตรวจสอบการเชื่อมต่อของคุณ"; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "คุณลักษณะนี้ยังไม่ได้รับการนำมาใช้งาน"; break;
                        case "ERR_EXEC-BATCH": resultat = "เกิดข้อผิดพลาดขณะดำเนินการแบทช์ไฟล์: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "ดูเหมือนว่าการเริ่ม Minecraft ไม่เป็นไปได้"; break;

                        case "WARN_WRONGFOLDER": resultat = "โฟลเดอร์ผิด: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "โปรแกรมต้องถูกเรียกใช้จากโฟลเดอร์ 'mods' ของ .minecraft ของคุณ"; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "ดูว่ามันทำอะไร..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "เปิดรายงานปัญหา"; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "เปิด URL ประวัติการเปลี่ยนแปลง, สรุปการเปลี่ยนแปลงต่อการอัปเดต"; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "เปิด GitHub ที่เก็บข้อมูล"; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "เปิด URL เพื่อดาวน์โหลด Fabric"; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "ดาวน์โหลด / อัปเดต mod"; break;

                        case "INFO_SUCCESS": resultat = "ทุกอย่างเสร็จสมบูรณ์แล้ว!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\nโดย MythMega\n2023\nเวอร์ชัน: {version}\nสร้าง: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "ฉันคือ MythMega (ฉันยังไม่ได้สร้างส่วนนี้, ไม่เร่งด่วน)"; break;
                        case "INFO_CONTRIBUTORS": resultat = $"ผู้มีส่วนร่วมคือดังต่อไปนี้:\n{getContributorsNamesAndLbl()}\nขอบคุณพวกเขา!"; break;

                        case "CONTRIBUTORS": resultat = "ผู้มีส่วนร่วม"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "ผู้แปล"; break;
                        case "CONTRIB_DEV": resultat = "การพัฒนา"; break;
                        case "CONTRIB_ARTIST": resultat = "ศิลปิน/การออกแบบ"; break;
                        case "CONTRIB_TESTER": resultat = "ผู้ทดสอบ"; break;
                        case "CONTRIB_OTHER": resultat = "อื่น ๆ"; break;

                        case "LBL_WARNING": resultat = "คำเตือน!"; break;
                        case "LBL_CONFIRMATION": resultat = "คุณแน่ใจหรือไม่ว่าต้องการดำเนินการนี้?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "ไม่เลือกทั้งหมด"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "เลือกทั้งหมด"; break;
                        case "LBL_STOP": resultat = "หยุด"; break;
                        case "LBL_OUI": resultat = "ใช่"; break;
                        case "LBL_NON": resultat = "ไม่ใช่"; break;
                        case "LBL_ANNULER": resultat = "ยกเลิก"; break;
                        case "LBL_LOCALISER": resultat = "ค้นหาตำแหน่ง"; break;

                        case "": resultat = ""; break;
                    }
                    break;

                // Indonesian
                case "ID":
                    switch (textCode)
                    {
                        case "ERR_NOMODS": resultat = "Terjadi kesalahan :( Pastikan perangkat lunak ini ditempatkan di folder 'mods' dari .minecraft Anda!\nJuga, pastikan Anda telah mengunduh mod-modnya!"; break;
                        case "ERR_TIMEOUT": resultat = "Waktu habis - Periksa koneksi Anda."; break;
                        case "ERR_NOTYETIMPLEMENTED": resultat = "Fitur ini belum diimplementasikan."; break;
                        case "ERR_EXEC-BATCH": resultat = "Terjadi kesalahan saat menjalankan berkas batch: "; break;
                        case "ERR_NOMINECRAFEXE": resultat = "Sepertinya meluncurkan Minecraft tidak mungkin."; break;

                        case "WARN_WRONGFOLDER": resultat = "Folder yang Salah: "; break;
                        case "WARN_MUSTSTARTINMODSFOLDER": resultat = "Perangkat lunak ini harus dijalankan dari folder 'mods' dari .minecraft Anda."; break;

                        case "MOUSEHOVER_BTNQUITTER": resultat = "Lihat apa yang dilakukannya..."; break;
                        case "MOUSEHOVER_BTNREPORT": resultat = "Buka laporan masalah."; break;
                        case "MOUSEHOVER_BTNCHANGELOG": resultat = "Buka URL catatan perubahan, ringkasan perubahan per pembaruan."; break;
                        case "MOUSEHOVER_BTNSOURCECODE": resultat = "Buka repositori GitHub."; break;
                        case "MOUSEHOVER_BTNFABRIC": resultat = "Buka URL untuk mengunduh Fabric."; break;
                        case "MOUSEHOVER_BTNUPDATE": resultat = "Unduh / Perbarui mod"; break;

                        case "INFO_SUCCESS": resultat = "Semuanya berjalan dengan sukses!"; break;
                        case "INFO_ABOUT": resultat = $"My Survival Mods\noleh MythMega\n2023\nVersi: {version}\nBuild: {app_build}"; break;
                        case "INFO_MYTHMEGA": resultat = "Saya adalah MythMega (Saya belum membuat bagian ini, kurang mendesak)."; break;
                        case "INFO_CONTRIBUTORS": resultat = $"Para kontributor adalah sebagai berikut:\n{getContributorsNamesAndLbl()}\nTerima kasih kepada mereka!"; break;

                        case "CONTRIBUTORS": resultat = "Kontributor"; break;
                        case "CONTRIB_TRANSLATOR": resultat = "Penerjemah"; break;
                        case "CONTRIB_DEV": resultat = "Pengembang"; break;
                        case "CONTRIB_ARTIST": resultat = "Seniman/Desain"; break;
                        case "CONTRIB_TESTER": resultat = "Penguji"; break;
                        case "CONTRIB_OTHER": resultat = "Lainnya"; break;

                        case "LBL_WARNING": resultat = "Peringatan!"; break;
                        case "LBL_CONFIRMATION": resultat = "Apakah Anda yakin ingin melakukan tindakan ini?"; break;
                        case "LBL_TOUT_SELECTIONNER": resultat = "Tidak Pilih Semua"; break;
                        case "LBL_TOUT_DESELECTIONNER": resultat = "Pilih Semua"; break;
                        case "LBL_STOP": resultat = "Berhenti"; break;
                        case "LBL_OUI": resultat = "Ya"; break;
                        case "LBL_NON": resultat = "Tidak"; break;
                        case "LBL_ANNULER": resultat = "Batal"; break;
                        case "LBL_LOCALISER": resultat = "Temukan"; break;

                        case "": resultat = ""; break;
                    }
                    break;
            }
            return resultat;
        }
    }
}
