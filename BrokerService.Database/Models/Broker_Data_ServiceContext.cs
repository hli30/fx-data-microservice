using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BrokerService.Database.Models
{
    public partial class Broker_Data_ServiceContext : DbContext
    {
        public Broker_Data_ServiceContext()
        {
        }

        public Broker_Data_ServiceContext(DbContextOptions<Broker_Data_ServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PriceCandle> PriceCandle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceCandle>(entity =>
            {
                entity.ToTable("price_candle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AskClose).HasColumnName("ask_close");

                entity.Property(e => e.AskHigh).HasColumnName("ask_high");

                entity.Property(e => e.AskLow).HasColumnName("ask_low");

                entity.Property(e => e.AskOpen).HasColumnName("ask_open");

                entity.Property(e => e.BidClose).HasColumnName("bid_close");

                entity.Property(e => e.BidHigh).HasColumnName("bid_high");

                entity.Property(e => e.BidLow).HasColumnName("bid_low");

                entity.Property(e => e.BidOpen).HasColumnName("bid_open");

                entity.Property(e => e.Granularity)
                    .IsRequired()
                    .HasColumnName("granularity");

                entity.Property(e => e.Instrument)
                    .IsRequired()
                    .HasColumnName("instrument")
                    .HasMaxLength(7);

                entity.Property(e => e.PriceTime)
                    .HasColumnName("price_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });
        }
    }
}
