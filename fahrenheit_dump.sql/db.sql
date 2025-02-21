--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.0

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: user_role; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.user_role AS ENUM (
    'super_admin',
    'high_level_admin',
    'low_level_admin',
    'user'
);


ALTER TYPE public.user_role OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Feedbacks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Feedbacks" (
    "Id" uuid NOT NULL,
    "Message" text NOT NULL,
    "UserId" uuid NOT NULL
);


ALTER TABLE public."Feedbacks" OWNER TO postgres;

--
-- Name: Fireplaces; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Fireplaces" (
    "Id" uuid NOT NULL,
    "FuelUsage" integer NOT NULL,
    "FireLevel" integer NOT NULL,
    "Name" text NOT NULL,
    "Price" integer NOT NULL,
    "ImagePath" text,
    "Description" text DEFAULT ''::text NOT NULL,
    "Article" text DEFAULT ''::text NOT NULL
);


ALTER TABLE public."Fireplaces" OWNER TO postgres;

--
-- Name: Pumps; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pumps" (
    "Id" uuid NOT NULL,
    "Brand" text NOT NULL,
    "Pressure" integer NOT NULL,
    "PowerSupply" integer NOT NULL,
    "ImagePath" text,
    "Name" text NOT NULL,
    "Price" integer NOT NULL,
    "Description" text DEFAULT ''::text NOT NULL,
    "Article" text DEFAULT ''::text NOT NULL
);


ALTER TABLE public."Pumps" OWNER TO postgres;

--
-- Name: Radiators; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Radiators" (
    "Id" uuid NOT NULL,
    "HeatedValue" double precision NOT NULL,
    "Material" text NOT NULL,
    "Name" text NOT NULL,
    "Price" integer NOT NULL,
    "ImagePath" text,
    "Description" text DEFAULT ''::text NOT NULL,
    "Article" text DEFAULT ''::text NOT NULL
);


ALTER TABLE public."Radiators" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "Email" text NOT NULL,
    "Role" smallint NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Name: WaterBoilers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."WaterBoilers" (
    "Id" uuid NOT NULL,
    "HeatedValue" integer DEFAULT 0 NOT NULL,
    "Material" text DEFAULT ''::text NOT NULL,
    "MaxTemperature" integer DEFAULT 0 NOT NULL,
    "Name" text NOT NULL,
    "Price" integer NOT NULL,
    "ImagePath" text,
    "Description" text DEFAULT ''::text NOT NULL,
    "Article" text DEFAULT ''::text NOT NULL
);


ALTER TABLE public."WaterBoilers" OWNER TO postgres;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Data for Name: Feedbacks; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Feedbacks" ("Id", "Message", "UserId") FROM stdin;
8b078e88-bb07-42e8-a0dc-e3bc2e136619	1231	c747f562-9e58-47b9-acc2-26dea82a9ae9
159db816-5ba2-4957-8341-eff1afc5ab41	У вас отличный сайт!	1b8bd2bf-c4a3-4098-9fe8-08fc7171b550
3db046ab-cb08-46a0-ac4b-69d9f3c08c0a	Все хорошо работает	1b8bd2bf-c4a3-4098-9fe8-08fc7171b550
44026766-00be-4c7d-907c-142766a4bdac	ссс	849230cc-86dc-4243-bb2d-cd3f348c00dd
\.


--
-- Data for Name: Fireplaces; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Fireplaces" ("Id", "FuelUsage", "FireLevel", "Name", "Price", "ImagePath", "Description", "Article") FROM stdin;
3014aceb-45ed-4538-90bb-ecb0548a4033	15	25	Портал Firelight Bricks Wood 25 (камень коричневый, шпон темный дуб)	31490	/uploads/fireplaces/b60867a1-95d7-43ac-ade5-26e66e5e4b6b.jpg	Bricks Wood 25	 Bricks Wood 25 BW
031c2328-db04-4ff4-ba03-646d03c099fb	20	45	Портал Firelight Canto Long (графит, черный)	35990	/uploads/Fireplace/eb968390-435e-4ba0-8487-a57ddd5e88d6.jpg	Портал Firelight Tetris 25 — это стильный и современный элемент для вашего интерьера, сочетающий в себе функциональность и элегантность. Сочетание белого и серого цветов делает портал универсальным и подходящим для различных стилей оформления помещений. Он идеально подходит для установки электрических каминов, создавая комфортную атмосферу в вашем доме.  Особенности:  Дизайн: Модульная конструкция с возможностью выбора цветов — белый и серый. Материал: Высококачественные, долговечные материалы, что гарантирует надежность и долговечность конструкции. Размер: Компактные размеры, которые подходят для большинства стандартных помещений. Удобство установки: Портал легко устанавливается и позволяет быстро создать уютную атмосферу с любым камином. Современный стиль: Подходит для различных интерьеров — от классических до современных. Портал Firelight Tetris 25 — это не только украшение вашего дома, но и надежная основа для любого камина.	Canto Long GB
1e04a601-facd-4582-b968-05541dfb9695	35	27	Портал Firelight Scala Classic (сланец скалистый серый, шпон венге)	60000	/uploads/Fireplace/18031611-e6a5-4221-b20c-9949252b5b2b.jpg	Серия — Canto Каминокомплект Firelight EFP/P-2520LS N  с порталом Canto 25 (белый)	set 3720LS N + Canto 25 W
7b818136-bd44-4093-a064-09309b2b7b96	16	20	Портал Firelight Tetris 25 	32380	/uploads/Fireplace/14cd3dce-0110-4852-8c2a-1c39d7c72cf6.jpg	Портал Firelight Tetris 25 — это стильное и современное решение для создания уютной атмосферы в вашем доме. Этот портал выполнен в элегантном сочетании белого и серого цветов, что позволяет гармонично вписать его в любой интерьер, от классического до современного. Он станет идеальной основой для установки электрического или биокамина, создавая не только тепло, но и атмосферу комфорта	Tetris 25 WG
47ffba1d-3253-423e-814e-0b912cd5d38a	25	35	Тумба с биокамином Firelight BFP-P1700L	85000	/uploads/Fireplace/992e6eb2-9c15-41d7-8359-9acbfda002b5.png	Тумба с биокамином Firelight BFP-P1700L (белый). Этот портал будет отличным выбором для тех, кто ищет стильное и функциональное решение для своего дома или офиса. 	BFP-P1700L W
11b53f82-3179-4a0f-85c6-55c0df663eb5	25	35	Каминокомплект Firelight EFP/P-2520LS N с порталом Canto 25 (белый)	44700	/uploads/Fireplace/e94c3a64-a80a-4a78-9d38-1877a046ba3b.jpg	Серия — Canto Каминокомплект Firelight EFP/P-2520LS N  с порталом Canto 25 (белый)	set 2520LS N + Canto 25 W
\.


--
-- Data for Name: Pumps; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Pumps" ("Id", "Brand", "Pressure", "PowerSupply", "ImagePath", "Name", "Price", "Description", "Article") FROM stdin;
3b2ad605-8c5e-45f9-928f-308c6d887193	Grundfos	1250	150	/uploads/pumps/ec2f2939-b5fd-4e45-8251-1d4a0cba3ce5.jpg	Циркуляционный насос Grundfos UPS 25-60 180 (1х230В)	14520	Гарантия, лет — 3 	96281477
ec4d05f2-ec53-486a-8431-dfdfe41638d4	Grundfos	1250	150	/uploads/pumps/a20221b8-a95f-4717-80c4-92957071230a.jpg	Grundfos SCALA Установка SCALA2 3-45 AKCCDE 1x200-240V 50/60Hz	47268	Grundfos SCALA Установка SCALA2 3-45 	99027073
f8e3cfa8-6f7c-469f-8ef7-f14fad9c9bc3	Grundfos	1250	150	/uploads/pumps/873ffbc9-895b-4471-a18b-9b54c2aa0f9c.jpg	Grundfos SBA Насос SBA 3-45 A	53000	Grundfos SBA Насос SBA 3-45 A Автоматическая погружная насосная установка, предназначена для водоснабжения. 	97896290
785bc890-72ab-41c6-be68-fd05120687c7	Wilo	1250	150	/uploads/pumps/56d2b6cb-9cca-4ccf-a99d-ace260281d84.jpg	Насос для повышения давления WILO PB 088EA	1523	Невероятный насос поможет вам!!!	3059251
fed7dc74-7f5f-4ad5-9740-151d0dbf824e	Grundfos	6	230	/uploads/Pump/bd0f083b-0691-4596-9835-3fa878effcdf.jpg	Grundfos Установка Sololift2 WC-3	66200	Grundfos Установка Sololift2 WC-3 Малогабаритная, полностью укомплектованная и готовая к монтажу насосная установка представляет собой герметично закрытый пластиковый резервуар в котором расположены - насос с двигателем “сухого исполнения” с обратным клапаном в напорном патрубке, профессиональный режущий механизм и мощный двигатель способные справиться даже с предметами личной гигиены (WC-1, WC-3, CWC-3), реле уровня, вентиляционный клапан с угольным фильтром улучшенного качества (не требуются никакие дополнительные фильтры), электрический кабель длиной 1.2 м со штекером Shuko. Насос автоматически включается при заполнении резервуара (уровень включения) и автоматически выключается при его опорожнении (уровня выключения). Применение: - унитаз напольный - раковина - душевая кабина - биде   Комплектация: переходники с одинаковым наружным диаметром и разными значениями в	97775315
122ef874-a714-4c73-a22a-74fce98294fc	Grundfos	7	220	/uploads/Pump/16120211-7761-4ee7-8229-c92e1cfe4d9e.jpg	Grundfos Установка Sololift2 C-3	41380	Grundfos Sololift2 C-3 — это компактная и мощная установка для перекачки сточных вод с системой отведения воды, оснащенная насосом с высокой производительностью. Sololift2 C-3 идеально подходит для использования в местах с ограниченным доступом к центральной канализации, таких как подвал, мансарда или другие вспомогательные помещения, где требуется установка дополнительной насосной системы. Установка оснащена системой для работы с бытовыми сточными водами с содержанием твердых частиц, что позволяет эффективно отводить воду из туалета и других сантехнических приборов.	97775315
000594f2-1cfa-4617-9f84-3a8aef6f3675	UNIPUMP	5	220	/uploads/Pump/529e79e8-0211-4d69-8f31-538bb7c569ae.jpg	Циркуляционный насос UNIPUMP UPC 32-80 180	9206	Циркуляционный насос UNIPUMP UPC 32-80 180 предназначен для эффективной циркуляции воды в системах отопления и горячего водоснабжения. Он обеспечивает стабильную работу отопительных систем в частных домах и коммерческих объектах, поддерживая оптимальное давление и поток воды в контуре. Благодаря своей прочной конструкции и высокому качеству материалов, насос обладает долгим сроком службы и хорошей энергоэффективностью.	UPC 32-80 180
16c56370-7a19-42cc-9d47-c43c5e7458b3	Хозяин	9	220	/uploads/Pump/8be4497b-cbf1-400f-88ca-ba8a3c2bcaad.jpg	Дренажный насос Хозяин НДП-400-35	43900	Насос оснащен надежным двигателем мощностью 400 Вт, что позволяет эффективно откачивать воду даже с мелким мусором до 35 мм в диаметре. Устройство имеет долговечную конструкцию и защиту от перегрева, что увеличивает срок службы и снижает риск поломок. Также насос оснащен удобной ручкой для транспортировки и легкостью в обслуживании.	1043
55412441-9e88-42b2-bad1-f85ca05feebd	Хозяин	4	220	/uploads/Pump/481ba594-8b16-45d0-835c-b119af26a39c.png	Скважинный насос Хозяин 4НГ-2.35	56800	Скважинный насос Хозяин 4НГ-2.35 предназначен для эксплуатации в системах водоснабжения, перекачки воды из скважин и колодцев. Этот насос идеально подходит для подачи воды на средние и большие глубины, обеспечивая эффективную работу при различных условиях эксплуатации.	3135
\.


--
-- Data for Name: Radiators; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Radiators" ("Id", "HeatedValue", "Material", "Name", "Price", "ImagePath", "Description", "Article") FROM stdin;
b6bb5ee8-c88d-48b9-be28-c1ae67ace12c	80	Алюминий	Радиатор алюминиевый STOUT Bravo 500 	4800	/uploads/radiators/610a71b4-cad8-488e-8a37-0a7c996429de.jpg	Радиатор алюминиевый STOUT Bravo 500, 4 секции, боковое подключение	SRA-0110-050004
78e14a09-7075-4a31-b722-ce5a77acf176	90	Биметалл	Биметаллический радиатор Rifar Base 500 на 6 секций	64840	/uploads/radiators/678bf9a1-a92f-4464-b2d3-9250d4b86468.png	Площадь отапливаемого помещения, кв.м. — 12 Межосевое расстояние, мм — 500	RB50006
c401a027-a262-4421-b9cc-368e9c2a536a	120	Биметалл	Радиатор биметаллический Royal Thermo Indigo Super+ 500	11700	/uploads/radiators/160fb4c2-cf54-46c8-8374-ee25bfa60bc5.png	Площадь отапливаемого помещения, кв.м. — 18 Межосевое расстояние, мм — 500	НС-1274311
8ada43f4-8b51-492f-92cf-d38d67c0dad2	140	Сталь	Биметаллический радиатор Rifar Base 500 на 12 секций	14850	/uploads/Radiator/eaed0876-2f0d-49b0-91f7-4a385560fab4.jpg	Площадь отапливаемого помещения, кв.м. — 12 Межосевое расстояние, мм — 50 Стальной трубчатый радиатор  Arbonia 3037/20 N69 твв с нижней подводкой	НС-3037/20 № 69 ventil oben RAL9016
7976acdb-6558-45c4-bb18-df8bdb15ee4d	10	Алюминиевый	Радиатор алюминиевый RIFAR Alum 350 x 4 секции	8250	/uploads/Radiator/6cc082c7-b547-402b-947f-0a82f19404e0.jpg	Площадь отапливаемого помещения, кв.м. — 12 Межосевое расстояние, мм — 50 Стальной трубчатый радиатор  Arbonia 3037/20 N69 твв с нижней подводкой	RAL35004
ff9f11d2-5d03-42d5-9f04-f215f66fbb0f	25	Корпус из стального покрытия с антикоррозийной обработкой, теплообменник из меди.	Настенный газовый конвектор Alpine Air NGS-20	37800	/uploads/Radiator/8f69ad30-148f-41d1-b4cd-8815feed0681.jpg	Его корпус выполнен из высококачественной стали с антикоррозийным покрытием, что продлевает срок службы устройства и гарантирует его долговечность. Внутренний теплообменник из меди обеспечивает отличную теплопередачу, а современная система управления позволяет легко поддерживать комфортную температуру в помещении.  Конвектор идеально подходит для установки в жилых домах, квартирах и офисах, обеспечивая стабильное отопление с низким уровнем шума и низким потреблением энергии. Оборудование имеет компактные размеры, что позволяет легко интегрировать его в любой интерьер.	375020
7fda3364-7201-4617-94ff-dd19a915ed3c	2200	Внутренний стальной каркас, наружные алюминиевые панели.	Биметаллический радиатор Rifar Base 500 на 12 секций	14850	/uploads/Radiator/32f3af66-1b28-4de7-aced-58193b4429b6.jpg	Биметаллический радиатор Rifar Base 500 на 12 секций — это идеальное решение для отопления как жилых, так и коммерческих помещений. Этот радиатор сочетает в себе прочность стали и легкость алюминия, обеспечивая высокую эффективность теплообмена и долгий срок службы.	RB50012
88bd8ffc-e57d-46f3-a6a8-0ecc7c981a9d	2200	Стальной Внутренний стальной каркас, наружные алюминиевые панели.	Биметаллический радиатор Rifar Base 500 на 12 секций	17982	/uploads/Radiator/09b39f2f-2739-48fe-ad82-d2fc2cfecd0f.png	Этот радиатор подходит для работы в системах с центральным отоплением, а также для частных домов и коттеджей, где важна высокая теплоотдача и долговечность. Благодаря современному дизайну и универсальным креплениям, он легко вписывается в любой интерьер. Rifar Base 500 также выделяется отличной устойчивостью к коррозии и механическим повреждениям.	 FTV220500501R2Y
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Id", "Name", "PhoneNumber", "PasswordHash", "Email", "Role") FROM stdin;
1b8bd2bf-c4a3-4098-9fe8-08fc7171b550	admin	1	8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918	admin	0
5e32158e-bf26-4a0b-a8ec-9f3855a70b9b	Сусарин Артур	4212312	a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3	susarin@mail.ru	0
c747f562-9e58-47b9-acc2-26dea82a9ae9	Елена Подвигина	89605854798	a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3	podvigina@mail.ru	0
b7f308ae-dcfd-408a-af58-a938df18ee12	Подвигина Елена	896058547123	a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3	elpodvigina@bk.ru	0
849230cc-86dc-4243-bb2d-cd3f348c00dd	string	string	473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8	string	0
\.


--
-- Data for Name: WaterBoilers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."WaterBoilers" ("Id", "HeatedValue", "Material", "MaxTemperature", "Name", "Price", "ImagePath", "Description", "Article") FROM stdin;
9cba565c-9eef-48d4-8093-6b6b922f9b2e	1200	Чугун	80	Проточный электрический водонагреватель Electrolux Smartfix 2.0 TS (3,5 kW)	3090	/uploads/waterBoilers/04e16022-32ca-43ff-beca-dadfdc056def.png	Проточный электрический водонагреватель Electrolux Smartfix 2.0 TS (3,5 kW) - кран+душ	НС-1017848
77dba5c5-1007-4f8b-8940-03afe4f01d99	150	Сплав	80	Проточный электрический водонагреватель Ariston AURES S 3.5 SH PL (душевая лейка)	3915	/uploads/waterBoilers/de8a2a52-2fb6-4a99-9821-6747b54960e4.jpg	Проточный электрический водонагреватель Ariston AURES S 3.5 SH PL (душевая лейка)	 3520016-V
dc06213d-4101-4e33-987a-ba25252911d5	2000	Сплав	80	Накопительный косвенный водонагреватель Baxi UBT 100	52715	/uploads/waterBoilers/d54be30f-8a22-4aaf-938f-411551b32839.png	Накопительный косвенный водонагреватель Baxi UBT 100	100020656
cf44fca6-ead1-4577-8610-6f580d8ca20e	12	Сталь с эмалированным покрытием	93	Бойлер косвенного нагрева VESSEN STIL 150	46000	/uploads/WaterBoiler/81bfe9d7-c74c-4b55-a333-62b4561a2704.png	Бойлер косвенного нагрева VESSEN STIL 150 — это надежное решение для обеспечения горячей водой в системах отопления и водоснабжения. Модель с объемом 150 литров и мощностью 12 кВт обеспечивает высокую эффективность нагрева воды, используя тепло от внешнего источника, например, котла. Эмалированная сталь в конструкции бойлера гарантирует долгий срок службы и защищает от коррозии. Этот бойлер может работать с максимальной температурой воды до 95°C, что позволяет эффективно нагревать воду для различных нужд. Простой и надежный в установке, он идеально подходит для использования в домах и квартирах с центральным отоплением.	APA150
a17b1ddd-526d-4959-b1cc-907e3374532b	1000	Сплав	100	Проточный электрический водонагреватель Ariston AURES M 7.7	6591	/uploads/waterBoilers/23c3dec4-545d-413a-b758-6c9cedf7973a.jpg	Страна производства — Вьетнам Гарантия, лет — 1	3195213
e9eb19ef-2772-4f82-9d91-3f1b9fcf3736	2	Нержавеющая сталь	90	Накопительный электрический водонагреватель Ariston ABSE VLS PRO INOX PW 80	22392	/uploads/WaterBoiler/6c0f82c8-4e5f-4a5e-a326-3b9fc74bb83b.png	Накопительный электрический водонагреватель Ariston ABSE VLS PRO INOX PW 80 — это надежное решение для быстрого и эффективного нагрева воды. Объем 80 литров идеально подходит для семьи средней величины или небольшой квартиры. Благодаря использованию нержавеющей стали в конструкции, водонагреватель отличается высокой прочностью и долговечностью. Максимальная температура воды достигает 80°C, что позволяет использовать устройство для бытовых нужд и хозяйственных нужд с комфортом. Водонагреватель оснащен высококачественными компонентами, что обеспечивает долгий срок службы и энергоэффективность. Этот модель отличается современным дизайном и простотой в эксплуатации, делая ваш быт более удобным и комфортным.	3700679
c36afac0-ced8-4bd6-83a2-b3156155f255	24	Нержавеющая сталь	85	Газовый водонагреватель Ariston SGA 200 R	72352	/uploads/WaterBoiler/4d5aea16-0c1b-41da-b636-33f1b1086839.jpeg	Газовый водонагреватель Ariston SGA 200 R — это эффективное и экономичное решение для нагрева воды в домах и квартирах. С мощностью 24 кВт, данный водонагреватель способен быстро и равномерно обогревать воду, обеспечивая бесперебойное снабжение горячей водой для семьи среднего размера. Нержавеющая сталь в конструкции устройства обеспечивает долговечность и устойчивость к коррозии, что продлевает срок службы водонагревателя. Максимальная температура нагрева составляет 75°C, что идеально подходит для большинства бытовых нужд. Компактный дизайн и легкость в установке делают этот водонагреватель отличным выбором для тех, кто ценит качество и эффективность в использовании газа.	7730
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20241028161444_Initial	8.0.10
20241029132012_PicUpdate	8.0.10
20241029133055_PicUpdate2	8.0.10
20241107093342_MakeImagePathNullable	8.0.10
20241107145559_AddImagePathToEveryEntity	8.0.10
20241109120508_AddEntitiesDescription	8.0.10
20241111091125_AddEntitiesArticle	8.0.10
20241111100230_ChangeUserAndFeedback	8.0.10
20241111100948_RenameClientToUser	8.0.10
\.


--
-- Name: Feedbacks PK_Feedbacks; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Feedbacks"
    ADD CONSTRAINT "PK_Feedbacks" PRIMARY KEY ("Id");


--
-- Name: Fireplaces PK_Fireplaces; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Fireplaces"
    ADD CONSTRAINT "PK_Fireplaces" PRIMARY KEY ("Id");


--
-- Name: Pumps PK_Pumps; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pumps"
    ADD CONSTRAINT "PK_Pumps" PRIMARY KEY ("Id");


--
-- Name: Radiators PK_Radiators; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Radiators"
    ADD CONSTRAINT "PK_Radiators" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- Name: WaterBoilers PK_WaterBoilers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."WaterBoilers"
    ADD CONSTRAINT "PK_WaterBoilers" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Feedbacks_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Feedbacks_UserId" ON public."Feedbacks" USING btree ("UserId");


--
-- Name: Feedbacks FK_Feedbacks_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Feedbacks"
    ADD CONSTRAINT "FK_Feedbacks_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

