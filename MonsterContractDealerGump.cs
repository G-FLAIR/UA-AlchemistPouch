            public ClaimListGump( AnimalTrainer trainer, Mobile from, List<BaseCreature> list ) : base( 25, 25 )
            {
                m_Trainer = trainer;
                m_From = from;
                m_List = list;

                from.CloseGump( typeof( ClaimListGump ) );

                this.Closable=true;
                this.Disposable=true;
                this.Dragable=true;
                this.Resizable=false;

                AddPage(0);
                const int height = 600;
                const int maxPetsPerColumn = 10;
                int petColumns = Math.Max(2, (int)Math.Ceiling((double)list.Count / maxPetsPerColumn)); // 10 pets per column

                int width = 420 + 200 * (petColumns - 1);

                // Card - Border
                AddImageTiled(0, 0, width, height, 155);

                // Card - Background
                const int padding = 2;
                AddImageTiled(padding, padding, width - 2 * padding, height - 2 * padding, 129);

                // Overlay images
                if (petColumns < 3)
                {
                    AddImage(7, 8, 133); // Top Left
                    AddImage(218, 47, 132); // Top Center
                    AddImage(380, 8, 134); // Top Right

                    AddImage(8, 517, 139); // Bottom Left
                    AddImage(164, 551, 140); // Bottom Center
                    AddImage(269, 342, 147); // Bottom Right
                }
                else // Good luck ... We don't expect more than 3 columns
                {
                    AddImage(7, 8, 133); // Top Left
                    AddImageTiled(218, 47, 400, 10, 132); // Top Center
                    AddImage(580, 8, 134); // Top Right

                    AddImage(28, 517, 139); // Bottom Left
                    AddImageTiled(195, 551, 400, 30, 140); // Bottom Center
                    AddImage(489, 342, 147); // Bottom Right
                }
...
            }


\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


...
                int maxCount = Server.Mobiles.AnimalTrainer.GetMaxStabled( from );
                AddHtml( 174, 68, 300, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG> " + list.Count + " OF " + maxCount + " PETS IN THE STABLE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

                for (int column = 0; column < petColumns; column++)
                {
                    int x = 105 + 220 * column;
                    int y = 95;

                    int offset = maxPetsPerColumn * column;
                    for (int i = 0; i < Math.Min(maxPetsPerColumn, list.Count - offset); ++i)
                    {
                        BaseCreature pet = list[i + offset];

                        if (pet == null || pet.Deleted)
                            continue;

                        y = y + 35;

                        AddHtml(x + 40, y, 165, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + pet.Name + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
                        AddButton(x, y, 4005, 4005, (i + offset + 1), GumpButtonType.Reply, 0);
                    }
                }
