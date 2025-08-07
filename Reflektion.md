# Reflektion – Övning 8: MovieAPI med flerlagerarkitektur

## 🔄 Hur skiljer sig detta från tidigare övningar?

Tidigare övningar byggde på enklare applikationer med en mer monolitisk struktur, där datamodeller, affärslogik och API låg i samma projekt. Det gjorde det snabbt att komma igång, men svårt att testa, återanvända eller vidareutveckla lösningen.

I denna övning har vi istället använt en **flerlagerarkitektur** med tydlig separation mellan:
- Entiteter och DTOs (`Movie.Core`)
- Databasåtkomst via repositories och UnitOfWork (`Movie.Data`)
- Affärslogik (`Movie.Services`)
- API-kontroller (`Movie.Presentation`)
- Tester (`Movie.Tests`)

Denna struktur ger en mer professionell kodbas och påminner om verkliga system.

---

## ✅ Vad är fördelarna med flerlagerarkitektur?

- **Separation of Concerns** – varje lager ansvarar för sitt område, vilket gör det lättare att förstå och underhålla.
- **Testbarhet** – det är enkelt att enhetstesta affärslogik utan att behöva köra hela API:t eller databasen.
- **Återanvändbarhet** – tjänstelagret kan användas från flera gränssnitt, t.ex. mobilappar eller andra API:er.
- **Skalbarhet** – nya funktioner kan byggas ut i rätt lager utan att påverka resten av systemet.
- **Bättre struktur** – tydlig organisation som gör teamarbete enklare och minskar risken för spagettikod.

---

## 💼 Hur kan du använda detta i ett riktigt projekt?

I ett professionellt projekt där flera utvecklare samarbetar är en flerlagerarkitektur nästan alltid nödvändig. Den möjliggör:
- Tydliga API-kontrakt mellan frontend och backend
- Kod som är lätt att testa, deploya och felsöka
- Möjlighet att skala upp med caching, autentisering, loggning etc.
- Integration med andra system eller externa API:er utan att störa kärnlogiken

Jag kan använda denna struktur som grund för alla kommande .NET-projekt, både privat och i arbetslivet.